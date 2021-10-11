using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Collect_Form;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Create_Collect;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Get_Collect;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Update_Collect;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Collect;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Join;
using user_stuff_share_app.Repository_Interfaces.ICollect_Repository;
using user_stuff_share_app.Repository_Interfaces.ICool_Repository;

namespace user_stuff_share_app.Controllers
{
    [Route("user/collect/")]
    [ApiController]
    [Authorize]
    public class CollectController : ControllerBase
    {
        private readonly ICollectRepository collectRepo;
        private readonly UserInfo userInfo;
        private readonly ICoolCollectRepository coolCollectRepo;

        public CollectController(ICollectRepository collectRepo, ICoolCollectRepository coolCollectRepo, UserInfo userInfo)
        {
            this.collectRepo = collectRepo;
            this.userInfo = userInfo;
            this.coolCollectRepo = coolCollectRepo;
        }

        /*
        [HttpGet("blah")]
        public async Task<IActionResult> GetSHit()
        {
            DateTime dateStart = DateTime.Now.AddDays(-30);
            DateTime dateEnd = DateTime.Now.AddDays(1).AddTicks(-1);
            var blah = await _db.Collect.Where(t => (t.Created >= dateStart && t.Created <= dateEnd)).OrderBy(c => c.Created)
                 .ToListAsync();
            return Ok(blah);
        }

       */


        // Create Collection
        [HttpPost]
        public async Task<IActionResult> CreateCollect([FromBody] ReqCreateCollect createCollect)
        {
            
            createCollect.UserId = userInfo.IdClaim(User);
            ResId createdCollect = await collectRepo.CreateCollect(createCollect);
            if (createdCollect == null)
            {
                ModelState.AddModelError("", $"Something went wrong saving {createCollect.Title} item!");
                return StatusCode(500, ModelState);
            }


            ReqCreateCoolCollect reqCreateCool = new () { CollectId = createdCollect.Id, Value=0};
            ResId createCool = await coolCollectRepo.CreateCoolCollect(reqCreateCool);
            if (createCool == null)
            {
                ModelState.AddModelError("", $"Something went wrong saving {createCollect.Title} item!");
                return StatusCode(500, ModelState);
            }

            /*
            ReqCreateCoolJoin reqCreateCoolJoin = new ReqCreateCoolJoin(){UserId = createCollect.UserId, CoolCollectId= createCool.Id, CollectId=createdCollect.Id };
            bool coolJoin = await coolCollectRepo.AddCoolUser( reqCreateCoolJoin);
            if (!coolJoin)
            {
                ModelState.AddModelError("", $"Something went wrong saving {createCollect.Title} item!");
                return StatusCode(500, ModelState);
            }
            */
            return Ok(createdCollect);
        }
    
        // Get ONE Collection
        [HttpPost("one")]
        public async Task<IActionResult> GetCollect([FromBody] ReqIdUserId reqIdUserId)
        {
            reqIdUserId.UserId = userInfo.IdClaim(User);

            ResGetCollect collect = await collectRepo.GetCollect(reqIdUserId);
          
            if (collect != null)
            {
                
                return Ok(collect);
            }
            else
            {
                return NotFound();
            }
        }

        // Get All User Collections
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCollects()
        {
            ReqUserId reqUserId = new ReqUserId { UserId = userInfo.IdClaim(User) };
        
                IEnumerable<ResGetCollect> collects = await collectRepo.GetAllCollects(reqUserId);
                if (collects == null)
                {
                    return NotFound();
                }
                return Ok(collects);
         }

        // Patch Collection
        [HttpPatch("uc")]
        public async Task<IActionResult> UpdateCollect([FromBody] ReqUpdateCollect reqUpdate)
        {
            reqUpdate.UserId = userInfo.IdClaim(User);

                bool updatedCollect = await collectRepo.UpdateCollect(reqUpdate);
                if (!updatedCollect)
                {
                    ModelState.AddModelError("", $"Something went wrong updating collection!");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }
        // Delete Collect
        [HttpPost("dc")]
        public async Task<IActionResult> DeleteCollect(ReqIdUserId reqIdUserId)
        {
            if (reqIdUserId == null)
            {
                return BadRequest(ModelState);
            }
            reqIdUserId.UserId = userInfo.IdClaim(User);
            bool deletedCollect = await collectRepo.DeleteCollect(reqIdUserId);

            if (deletedCollect)
            {
                return NoContent();
            }
          return NotFound();
        }
        
            // Update Collect Form
            [HttpPatch ("ucf")]
            public async Task<IActionResult> UpdateForm([FromBody] ReqUpdateCF updateForm)
            {
                if (updateForm == null)
                {
                    return BadRequest(ModelState);
                }
            
            updateForm.UserId = userInfo.IdClaim(User);

            bool updatedForm = await collectRepo.UpdateCollectForm(updateForm);
                if (updatedForm)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500);
                }
            }
    }
}

