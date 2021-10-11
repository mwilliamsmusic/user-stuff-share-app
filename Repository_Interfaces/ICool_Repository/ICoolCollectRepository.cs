using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Collect;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Join;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Update_Cool_Collect;

namespace user_stuff_share_app.Repository_Interfaces.ICool_Repository
{
    public interface ICoolCollectRepository
    {
        Task<ResId> CreateCoolCollect(ReqCreateCoolCollect reqCreateCool);
        Task<bool> CoolUserExists(ReqUpdateCoolCollect reqUpdateCoolCollect);
        Task<bool> UpdateValueCoolCollect(ReqUpdateCoolCollect reqUpdateCool);
        Task<bool> AddCoolUser(ReqCreateCoolJoin reqCreateCoolJoin);
    }
}
