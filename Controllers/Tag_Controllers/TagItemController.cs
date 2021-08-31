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
        private readonly ITagItemRepository tagRepo;
        private readonly StatusMessages _statusMessage;
        public TagItemController(ITagItemRepository tagRepo, StatusMessages statusMessaage)
        {
            this.tagRepo = tagRepo;
            _statusMessage = statusMessaage;

        }



            [HttpPost("ait")]
            public async Task<IActionResult> AddItemTag([FromBody] ReqAddTagItemHandler reqAddTagItemHandler)
            {
                if (reqAddTagItemHandler == null)
                {
                    return BadRequest();
                }
            bool check = await tagRepo.CheckItemTagJoin(reqAddTagItemHandler);
            if (check)
            {
                return Conflict(_statusMessage.TagJoinCheck());
            }
            bool addTags = await tagRepo.AddItemTagHandler(reqAddTagItemHandler);
                if (!addTags)
                {
                    return BadRequest();
                }
                return NoContent();
            }


        [HttpPost("git")]
        public async Task<IActionResult> GetItemTag([FromBody] ReqItemId reqItemId)
        {
            if (reqItemId == null)
            {
                return BadRequest();
            }

            IEnumerable<ResTagId> getCollectTags = await tagRepo.GetItemTags(reqItemId);
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

            bool getCollectTags = await tagRepo.RemoveItemTagJoin(reqRemoveItemTag);
            if (!getCollectTags)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
