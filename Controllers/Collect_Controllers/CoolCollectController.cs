using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Join;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Update_Cool_Collect;
using user_stuff_share_app.Repository_Interfaces.ICool_Repository;
using user_stuff_share_app.Status_Messages;

namespace user_stuff_share_app.Controllers.Collect_Controllers
{
    [Route("user/cool/collect")]
    [ApiController]
    [Authorize]
    public class CoolCollectController : ControllerBase
    {
        private readonly ICoolCollectRepository _coolCollectRepo;
        private readonly ApplicationDbContext _db;
        private readonly UserInfo _userInfo;

        private readonly StatusMessages _statusMessages;




        public CoolCollectController(ICoolCollectRepository coolCollectRepo, ApplicationDbContext db, UserInfo userInfo, StatusMessages statusMessages)
        {
            _coolCollectRepo = coolCollectRepo;
            _db = db;
            _userInfo = userInfo;
            _statusMessages = statusMessages;
        }
        

        
        [HttpPost("up")]
        public async Task<IActionResult> UpdateCool([FromBody]  ReqUpdateCoolCollect reqUpdateCoolCollect)
        {
            reqUpdateCoolCollect.UserId = _userInfo.IdClaim(User);
            /*
            if (!String.IsNullOrEmpty(reqUpdateCoolCollect.ToString()))
            {
                return Unauthorized();
            }
            */
              bool didVote = await _coolCollectRepo.CoolUserExists(reqUpdateCoolCollect);
            if (!didVote)
            {
                bool updateCool = await _coolCollectRepo.UpdateValueCoolCollect(reqUpdateCoolCollect);
                if (updateCool)
                {
                    ReqCreateCoolJoin join = new() { CollectId = reqUpdateCoolCollect.CollectId, UserId = reqUpdateCoolCollect.UserId, CoolCollectId = reqUpdateCoolCollect.Id };
                    bool addJoin = await _coolCollectRepo.AddCoolUser(join);
                    if (addJoin)
                    {

                        return NoContent();
                    }
                    return StatusCode(500);
                }
                return Conflict(_statusMessages.TagAlreadyCool());
            }
            return NotFound();
 }
        }
    }

