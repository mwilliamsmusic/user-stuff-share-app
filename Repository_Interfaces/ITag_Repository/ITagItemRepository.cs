using System.Collections.Generic;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Item.Tag_Item_Dtos;

namespace user_stuff_share_app.Repository_Interfaces.ITag_Repository
{
    public interface ITagItemRepository
    {
        Task<IEnumerable<ResTagId>> GetItemTags(ReqItemId reqItemId);
        Task<bool> AddItemTagHandler(ReqAddTagItemHandler reqAddTagHandler);
        Task<bool> RemoveItemTagJoin(ReqId reqId);
    }
}
