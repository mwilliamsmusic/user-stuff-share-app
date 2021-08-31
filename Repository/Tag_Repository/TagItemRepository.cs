using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ssa_database.Models.Tag_Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Item.Tag_Item_Dtos;
using user_stuff_share_app.Repository_Interfaces.ITag_Repository;

namespace user_stuff_share_app.Repository.Tag_Repository
{
    public class TagItemRepository: ITagItemRepository
    {
        private readonly ApplicationDbContext dbJoin;
        private readonly ApplicationDbContext dbTag;
        private readonly IMapper mapper;

        public TagItemRepository(ApplicationDbContext dbTag, ApplicationDbContext dbJoin, IMapper mapper)
        {
            this.dbJoin = dbJoin;
            this.dbTag = dbTag;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ResTagId>> GetItemTags(ReqItemId reqItemId)
        {
            IEnumerable<TagItemJoin> items = await dbJoin.TagItemJoin.AsNoTracking().Where(c => c.ItemId == reqItemId.ItemId)
                  .OrderBy(c => c.TagName).ToListAsync();
            IEnumerable<ResTagId> res = mapper.Map<IEnumerable<ResTagId>>(items);
            return res;
        }

        public async Task<bool> AddItemTagHandler(ReqAddTagItemHandler reqAddTagHandler)
        {
            bool tags = await AddItemTag(reqAddTagHandler);
            bool joinTags = await AddItemTagJoin(reqAddTagHandler);

            return tags && joinTags ? true : false; ;
        }
        private async Task<bool> AddItemTag(ReqAddTagItemHandler reqAddTagHandler)
        {
            Tag req = new Tag { Name = reqAddTagHandler.TagName };
            bool tag = await dbTag.Tag.AsNoTracking().AnyAsync(t => t.Name == req.Name);
            if (!tag)
            {
                dbTag.Tag.Add(req);
                return await SaveTag();
            }
            return true;
        }
        private async Task<bool> AddItemTagJoin(ReqAddTagItemHandler reqAddTagHandler)
        {
            TagItemJoin tag = new TagItemJoin { ItemId = reqAddTagHandler.ItemId, TagName = reqAddTagHandler.TagName };
            dbJoin.TagItemJoin.Add(tag);
            return await SaveJoin();
        }

        public async Task<bool> RemoveItemTagJoin(ReqRemoveItemTag reqRemoveItemTag)
        {
            TagItemJoin tagJoin = await dbJoin.TagItemJoin
                .FirstOrDefaultAsync(c => c.ItemId == reqRemoveItemTag.ItemId && c.TagName == reqRemoveItemTag.TagName);
            dbJoin.TagItemJoin.Remove(tagJoin);
            return await SaveJoin();
        }

        public async Task<bool> CheckItemTagJoin(ReqAddTagItemHandler reqAddTagItemHandler)
        {
            bool tag = await dbTag.TagItemJoin.AsNoTracking().AnyAsync(t => t.ItemId == reqAddTagItemHandler.ItemId && t.TagName == reqAddTagItemHandler.TagName);
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

