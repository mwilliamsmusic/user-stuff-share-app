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
        private readonly ApplicationDbContext _dbJoin;
        private readonly ApplicationDbContext _dbTag;
        private readonly ApplicationDbContext _dbCollect;
        private readonly IMapper _mapper;

        public TagCollectRepository(ApplicationDbContext dbTag, ApplicationDbContext dbJoin, ApplicationDbContext dbCollect, IMapper mapper)
        {
            _dbJoin = dbJoin;
            _dbTag = dbTag;
            _mapper = mapper;
            _dbCollect = dbCollect;
        }

        public async Task<IEnumerable<ResIdTagName>> GetCollectTags(ReqCollectId reqCollectId)
        {
           IEnumerable<TagCollectJoin> collects = await _dbJoin.TagCollectJoin.AsNoTracking().Where(c => c.CollectId == reqCollectId.CollectId)
                 .OrderBy(c => c.TagName).ToListAsync();
            IEnumerable<ResIdTagName> res = _mapper.Map<IEnumerable<ResIdTagName>>(collects);
            return res;
        }

        public async Task<ResIdTagName> AddTagCollectHandler(ReqAddTagCollectHandler reqAddTagCollectHandler)
        {
            bool tags = await AddTagCollect(reqAddTagCollectHandler);
            if (tags)
            {
                ResIdTagName joinTag = await AddTagCollectJoin(reqAddTagCollectHandler);
                return joinTag; 
            }
            return null;
        }
    
        private async Task<bool> AddTagCollect(ReqAddTagCollectHandler reqAddTagCollectHandler)
        {
            Tag req = new Tag { Name = reqAddTagCollectHandler.TagName };
          bool tag = await  _dbTag.Tag.AsNoTracking().AnyAsync(t=> t.Name == req.Name);
            if (!tag)
            {
                _dbTag.Tag.Add(req);
                return await SaveTag();
            }
            return true;
        }
        private async Task<ResIdTagName> AddTagCollectJoin(ReqAddTagCollectHandler reqAddTagCollectHandler) {
            TagCollectJoin tag = new TagCollectJoin { TagName = reqAddTagCollectHandler.TagName, CollectId = reqAddTagCollectHandler.CollectId };
            _dbJoin.TagCollectJoin.Add(tag);
           await SaveJoin();
            ResIdTagName res = new() { Id = tag.Id, TagName = tag.TagName };
            return res;
        }

        public async Task<bool> RemoveTagCollectJoin(ReqRemoveCollectTag reqRemoveCollectTag) 
        {
            TagCollectJoin tagJoin = await  _dbJoin.TagCollectJoin
                .FirstOrDefaultAsync(c => c.Id == reqRemoveCollectTag.Id);
            _dbJoin.TagCollectJoin.Remove(tagJoin);

            return await SaveJoin();
        }

        public async Task<bool> CheckCollectTagJoin(ReqAddTagCollectHandler reqAddTagCollectHandler)
        {
            bool tag = await _dbTag.TagCollectJoin.AsNoTracking().AnyAsync(t => t.CollectId == reqAddTagCollectHandler.CollectId && t.TagName == reqAddTagCollectHandler.TagName);
            return tag;
        }
        public async Task<bool> CheckUser(long id, long userId)
        {
            bool tag = await _dbCollect.Collect.AsNoTracking().AnyAsync(t => t.Id == id && t.UserId == userId);
            return tag;
        }
        private async Task<bool> SaveTag()
        {
              return await _dbTag.SaveChangesAsync() >= 0 ? true : false; ;
        }

        private async Task<bool> SaveJoin()
        {
            return await _dbJoin.SaveChangesAsync() >= 0 ? true : false; ;
        }
    }
}
