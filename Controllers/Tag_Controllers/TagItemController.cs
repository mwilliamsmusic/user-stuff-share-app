using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Item.Tag_Item_Dtos;
using user_stuff_share_app.Repository_Interfaces.ITag_Repository;
using user_stuff_share_app.Status_Messages;

namespace user_stuff_share_app.Controllers.Tag_Controllers
{
    [Route("user/tag/item/")]
    [Authorize]
    [ApiController]
    public class TagItemController : ControllerBase
    {
        private readonly ITagItemRepository _tagRepo;
        private readonly StatusMessages _statusMessage;
        private readonly UserInfo _userInfo;
        public TagItemController(ITagItemRepository tagRepo, StatusMessages statusMessaage,UserInfo userInfo)
        {
           _tagRepo = tagRepo;
            _statusMessage = statusMessaage;
            _userInfo = userInfo;

        }



            [HttpPost("ait")]
            public async Task<IActionResult> AddItemTag([FromBody] ReqAddTagItemHandler reqAddTagItemHandler)
            {
                if (reqAddTagItemHandler == null)
                {
                    return BadRequest();
                }
            reqAddTagItemHandler.UserId = _userInfo.IdClaim(User);
            bool checkUser = await _tagRepo.CheckUser(reqAddTagItemHandler.ItemId, reqAddTagItemHandler.UserId);
            if (!checkUser)
            {
                return Unauthorized();
            }
            /*
        bool check = await _tagRepo.CheckItemTagJoin(reqAddTagItemHandler);
        if (check)
        {
            return Conflict(_statusMessage.TagJoinCheck());
        }
            */
            ResIdTagName addTag = await _tagRepo.AddItemTagHandler(reqAddTagItemHandler);
                if (addTag == null)
                {
                    return BadRequest();
                }
                return Ok(addTag);
            }


        [HttpPost("git")]
        public async Task<IActionResult> GetItemTag([FromBody] ReqItemId reqItemId)
        {
            if (reqItemId == null)
            {
                return BadRequest();
            }

            IEnumerable<ResIdTagName> getCollectTags = await _tagRepo.GetItemTags(reqItemId);
            if (getCollectTags == null)
            {
                return BadRequest();
            }
            return Ok(getCollectTags);
        }


        [HttpPost("rit")]
        public async Task<IActionResult> RemoveItemTag([FromBody] ReqRemoveItemTag reqRemoveItemTag)
        {
            if (reqRemoveItemTag == null)
            {
                return BadRequest();
            }
            reqRemoveItemTag.UserId = _userInfo.IdClaim(User);
            bool checkUser = await _tagRepo.CheckUser(reqRemoveItemTag.ItemId, reqRemoveItemTag.UserId);
            if (!checkUser)
            {
                return Unauthorized();
            }

            bool getCollectTags = await _tagRepo.RemoveItemTagJoin(reqRemoveItemTag);
            if (!getCollectTags)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
