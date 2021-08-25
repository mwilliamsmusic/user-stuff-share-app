using System.Collections.Generic;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Create_Item;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Get_Item;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Item_Form;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Update_Item;

namespace user_stuff_share_app.Repository_Interfaces.ICollect_Repository
{
    public interface IItemRepository
    {
        Task<ResId> CreateItem(ReqCreateItem reqCreateItem);

        Task<ResGetItem> GetItem(ReqId reqId);

        Task<IEnumerable<ResGetItem>> GetAllItems(ReqCollectId reqCollectId);

        Task<bool> UpdateItemCollect(ReqUpdateItem reqUpdate);

        Task<bool> DeleteItemCollect(ReqId reqId);

        Task<bool> Save();

        Task<bool> UpdateItemForm(ReqUpdateIF reqUpdate);
    }
}
