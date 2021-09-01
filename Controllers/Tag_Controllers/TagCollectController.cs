using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Collect.Tag_Collect_Dtos;
using user_stuff_share_app.Repository_Interfaces.ITag_Repository;
using user_stuff_share_app.Status_Messages;

namespace user_stuff_share_app.Controllers.Tag_controllers
{
    [Route("tag/collect")]
    [Authorize]
    [ApiController]
    public class TagCollectController : ControllerBase
    {
        private readonly ITagCollectRepository _tagRepo;
        private readonly StatusMessages _statusMessages;
        private readonly UserInfo _userInfo;
        public TagCollectController(ITagCollectRepository tagRepo, StatusMessages statusMessaages,  UserInfo userInfo)
        {
            _tagRepo = tagRepo;
            _statusMessages = statusMessaages;
            _userInfo = userInfo;
        }

        [HttpPost("act")]
        public async Task<IActionResult> AddCollectTag([FromBody] ReqAddTagCollectHandler reqAddTagCollectHandler)
        {
            if (reqAddTagCollectHandler == null )
            {
                return BadRequest();
            }

            reqAddTagCollectHandler.UserId = _userInfo.IdClaim(User);
            bool checkUser = await _tagRepo.CheckUser(reqAddTagCollectHandler.CollectId, reqAddTagCollectHandler.UserId);
            if (!checkUser)
            {
                return Unauthorized();
            }
      
            ResIdTagName addTag = await _tagRepo.AddTagCollectHandler(reqAddTagCollectHandler);
            if (addTag == null)
            {
                return BadRequest();
            }
            return Ok(addTag);
        }

        [HttpPost("gct")]
        public async Task<IActionResult> GetCollectTag([FromBody] ReqCollectId reqCollectId)
        {
         
            if (reqCollectId == null)
            {
                return BadRequest();
            }

           IEnumerable<ResIdTagName>  getCollectTags = await _tagRepo.GetCollectTags(reqCollectId);
            if (getCollectTags == null)
            {
                return BadRequest();
            }
            return Ok(getCollectTags);
        }


        [HttpPost("rct")]
        public async Task<IActionResult> RemoveCollectTag([FromBody] ReqRemoveCollectTag reqRemoveCollectTag)
        {
            if (reqRemoveCollectTag == null)
            {
                return BadRequest();
            }

            reqRemoveCollectTag.UserId = _userInfo.IdClaim(User);
            bool checkUser = await _tagRepo.CheckUser(reqRemoveCollectTag.CollectId, reqRemoveCollectTag.UserId);
            if (!checkUser)
            {
                return Unauthorized();
            }

            bool getCollectTags = await _tagRepo.RemoveTagCollectJoin(reqRemoveCollectTag);
            if (!getCollectTags)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
