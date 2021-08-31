using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ssa_database.Models.Tag_Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Collect.Tag_Collect_Dtos;
using user_stuff_share_app.Repository_Interfaces.ITag_Repository;

namespace user_stuff_share_app.Repository.Tag_Repository
{
    public class TagCollectRepository : ITagCollectRepository
    {
        private readonly ApplicationDbContext dbJoin;
        private readonly ApplicationDbContext dbTag;
        private readonly IMapper mapper;

        public TagCollectRepository(ApplicationDbContext dbTag, ApplicationDbContext dbJoin, IMapper mapper)
        {
            this.dbJoin = dbJoin;
            this.dbTag = dbTag;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ResTagId>> GetCollectTags(ReqCollectId reqCollectId)
        {
           IEnumerable<TagCollectJoin> collects = await dbJoin.TagCollectJoin.AsNoTracking().Where(c => c.CollectId == reqCollectId.CollectId)
                 .OrderBy(c => c.TagName).ToListAsync();
            IEnumerable<ResTagId> res = mapper.Map<IEnumerable<ResTagId>>(collects);
            return res;
        }

        public async Task<bool> AddTagCollectHandler(ReqAddTagCollectHandler reqAddTagCollectHandler)
        {
            bool tags = await AddTagCollect(reqAddTagCollectHandler);
            bool joinTags = await AddTagCollectJoin(reqAddTagCollectHandler);
      
             return tags && joinTags ? true : false; ;
        }
    
        private async Task<bool> AddTagCollect(ReqAddTagCollectHandler reqAddTagCollectHandler)
        {
            Tag req = new Tag { Name = reqAddTagCollectHandler.TagName };
          bool tag = await  dbTag.Tag.AsNoTracking().AnyAsync(t=> t.Name == req.Name);
            if (!tag)
            {
                dbTag.Tag.Add(req);
                return await SaveTag();
            }
            return true;
        }
        private async Task<bool> AddTagCollectJoin(ReqAddTagCollectHandler reqAddTagCollectHandler) {
            TagCollectJoin tag = new TagCollectJoin { TagName = reqAddTagCollectHandler.TagName, CollectId = reqAddTagCollectHandler.CollectId };
            dbJoin.TagCollectJoin.Add(tag);
            return await SaveJoin();
        }

        public async Task<bool> RemoveTagCollectJoin(ReqRemoveCollectTag reqRemoveCollectTag) 
        {
            TagCollectJoin tagJoin = await  dbJoin.TagCollectJoin
                .FirstOrDefaultAsync(c => c.CollectId == reqRemoveCollectTag.CollectId && c.TagName == reqRemoveCollectTag.TagName);
            dbJoin.TagCollectJoin.Remove(tagJoin);

            return await SaveJoin();
        }

        public async Task<bool> CheckCollectTagJoin(ReqAddTagCollectHandler reqAddTagCollectHandler)
        {
            bool tag = await dbTag.TagCollectJoin.AsNoTracking().AnyAsync(t => t.CollectId == reqAddTagCollectHandler.CollectId && t.TagName == reqAddTagCollectHandler.TagName);
            return tag;
        }

        private async Task<bool> SaveTag()
        {
              return await dbTag.SaveChangesAsync() >= 0 ? true : false; ;
        }

        private async Task<bool> SaveJoin()
        {
            return await dbJoin.SaveChangesAsync() >= 0 ? true : false; ;
        }
    }
}
