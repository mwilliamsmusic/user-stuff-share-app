using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Join;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Update_Cool_Collect;
using user_stuff_share_app.Repository_Interfaces.ICool_Repository;

namespace user_stuff_share_app.Controllers.Collect_Controllers
{
    [Route("user/cool/collect/")]
    [ApiController]
    [Authorize]
    public class CoolCollectController : ControllerBase
    {
        private readonly ICoolCollectRepository _coolCollectRepo;
        private readonly ApplicationDbContext _db;
        private readonly UserInfo _userInfo;

        public CoolCollectController(ICoolCollectRepository coolCollectRepo, ApplicationDbContext db, UserInfo userInfo)
        {
            _coolCollectRepo = coolCollectRepo;
            _db = db;
            _userInfo = userInfo;
        }
        

        
        [HttpPost("up")]
        public async Task<IActionResult> UpdateCool([FromBody]  ReqUpdateCoolCollect reqUpdateCool)
        {
            reqUpdateCool.UserId = _userInfo.IdClaim(User);
            if (!String.IsNullOrEmpty(reqUpdateCool.ToString()))
            {
                return Unauthorized();
            }
              bool didVote = await _coolCollectRepo.CoolUserExists(reqUpdateCool.UserId);
            if (!didVote)
            {
                bool updateCool = await _coolCollectRepo.UpdateValueCoolCollect(reqUpdateCool);
                if (updateCool)
                {
                    ReqCreateCoolJoin join = new ReqCreateCoolJoin();
                    join.CoolCollectId = reqUpdateCool.Id;
                    join.UserId = reqUpdateCool.UserId;
                  bool addJoin = await  _coolCollectRepo.AddCoolUser(join);
                    return NoContent();
                }
                return NotFound();
            }
            return NotFound();
        }
    }
}
