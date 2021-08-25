using System.Collections.Generic;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Collect_Form;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Create_Collect;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Get_Collect;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Update_Collect;

namespace user_stuff_share_app.Repository_Interfaces.ICollect_Repository
{

    public interface ICollectRepository
        {
        Task<ResId> CreateCollect(ReqCreateCollect reqCreateCollect);
        Task<ResGetCollect> GetCollect(ReqIdUserId reqIdUserId);
        Task<IEnumerable<ResGetCollect>> GetAllCollects(ReqUserId reqUserId);
        Task<bool> UpdateCollect(ReqUpdateCollect reqUpdate);
        Task<bool> UpdateCollectForm(ReqUpdateCF updateForm);
        Task<bool> DeleteCollect(ReqIdUserId reqIdUserId);
    }
}
