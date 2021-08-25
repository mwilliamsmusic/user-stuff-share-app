using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Collect.Tag_Collect_Dtos;
using user_stuff_share_app.Repository_Interfaces.ITag_Repository;

namespace user_stuff_share_app.Controllers.Tag_controllers
{
    [Route("tag/collect")]
    [Authorize]
    [ApiController]
    public class TagCollectController : ControllerBase
    {
        private readonly ITagCollectRepository tagRepo;
        public TagCollectController(ITagCollectRepository tagRepo)
        {
            this.tagRepo = tagRepo;
        }

        [HttpPost("act")]
        public async Task<IActionResult> AddCollectTag([FromBody] ReqAddTagCollectHandler reqAddTagCollectHandler)
        {
            if (reqAddTagCollectHandler == null )
            {
                return BadRequest();
            }
            
               bool addTags = await tagRepo.AddTagCollectHandler(reqAddTagCollectHandler);
            if (!addTags)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPost("gct")]
        public async Task<IActionResult> GetCollectTag([FromBody] ReqCollectId reqCollectId)
        {
         
            if (reqCollectId == null)
            {
                return BadRequest();
            }

           IEnumerable<ResTagId>  getCollectTags = await tagRepo.GetCollectTags(reqCollectId);
            if (getCollectTags == null)
            {
                return BadRequest();
            }
            return Ok(getCollectTags);
        }


        [HttpPost("rct")]
        public async Task<IActionResult> RemoveCollectTag([FromBody] ReqId reqId)
        {

            if (reqId == null)
            {
                return BadRequest();
            }

            bool getCollectTags = await tagRepo.RemoveTagCollectJoin(reqId);
            if (!getCollectTags)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
    
 

}
