using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Create_Cool_Item;
using user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Create_Cool_Item_Join;
using user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Update_Cool_Item;

namespace user_stuff_share_app.Repository_Interfaces.ICool_Repository
{
    public interface ICoolItemRepository
    {
        Task<ResId> CreateCoolItem(ReqCreateCoolItem reqCreateCool);
        Task<bool> UpdateValueCool(ReqUpdateCoolItem reqUpdateCool);
        Task<bool> AddCoolUser(ReqCreateCoolItemJoin reqCreateCoolJoin);
        Task<bool> CoolUserExists(long userId);
    }
}
