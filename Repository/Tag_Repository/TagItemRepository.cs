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
        private readonly ApplicationDbContext _dbJoin;
        private readonly ApplicationDbContext _dbTag;
        private readonly ApplicationDbContext _dbItem;
        private readonly IMapper _mapper;

        public TagItemRepository(ApplicationDbContext dbTag, ApplicationDbContext dbJoin, ApplicationDbContext dbItem, IMapper mapper)
        {
            _dbJoin = dbJoin;
            _dbTag = dbTag;
            _dbItem = dbItem;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResIdTagName>> GetItemTags(ReqItemId reqItemId)
        {
            IEnumerable<TagItemJoin> items = await _dbJoin.TagItemJoin.AsNoTracking().Where(c => c.ItemId == reqItemId.ItemId)
                  .OrderBy(c => c.TagName).ToListAsync();
            IEnumerable<ResIdTagName> res = _mapper.Map<IEnumerable<ResIdTagName>>(items);
            return res;
        }

        public async Task<ResIdTagName> AddItemTagHandler(ReqAddTagItemHandler reqAddTagHandler)
        {
            bool tag = await AddItemTag(reqAddTagHandler);
            if (tag)
            {
                ResIdTagName joinTag = await AddItemTagJoin(reqAddTagHandler);
                return joinTag;
            }
            return null;
        }
        private async Task<bool> AddItemTag(ReqAddTagItemHandler reqAddTagHandler)
        {
            Tag req = new Tag { Name = reqAddTagHandler.TagName };
            bool tag = await _dbTag.Tag.AsNoTracking().AnyAsync(t => t.Name == req.Name);
            if (!tag)
            {
                _dbTag.Tag.Add(req);
                return await SaveTag();
            }
            return true;
        }
        private async Task<ResIdTagName> AddItemTagJoin(ReqAddTagItemHandler reqAddTagHandler)
        {
            TagItemJoin tag = new TagItemJoin { ItemId = reqAddTagHandler.ItemId, TagName = reqAddTagHandler.TagName };
            _dbJoin.TagItemJoin.Add(tag);
            await SaveJoin();
            ResIdTagName res = new() { Id = tag.Id, TagName = tag.TagName };
            return res;
        }

        public async Task<bool> RemoveItemTagJoin(ReqRemoveItemTag reqRemoveItemTag)
        {
            TagItemJoin tagJoin = await _dbJoin.TagItemJoin
                .FirstOrDefaultAsync(c => c.Id == reqRemoveItemTag.Id);
            _dbJoin.TagItemJoin.Remove(tagJoin);
            return await SaveJoin();
        }

        public async Task<bool> CheckItemTagJoin(ReqAddTagItemHandler reqAddTagItemHandler)
        {
            bool tag = await _dbTag.TagItemJoin.AsNoTracking().AnyAsync(t => t.ItemId == reqAddTagItemHandler.ItemId && t.TagName == reqAddTagItemHandler.TagName);
            return tag;
        }

        public async Task<bool> CheckUser(long id, long userId)
        {
            bool tag = await _dbItem.Item.AsNoTracking().AnyAsync(t => t.Id == id && t.UserId == userId);
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

