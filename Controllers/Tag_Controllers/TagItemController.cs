using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Item.Tag_Item_Dtos;
using user_stuff_share_app.Repository_Interfaces.ITag_Repository;

namespace user_stuff_share_app.Controllers.Tag_Controllers
{
    [Route("tag/item/collect")]
    [Authorize]
    [ApiController]
    public class TagItemController : ControllerBase
    {
        private readonly ITagItemRepository tagRepo;
        public TagItemController(ITagItemRepository tagRepo)
        {
            this.tagRepo = tagRepo;
        }



            [HttpPost("ait")]
            public async Task<IActionResult> AddItemTag([FromBody] ReqAddTagItemHandler reqAddTagHandler)
            {
                if (reqAddTagHandler == null)
                {
                    return BadRequest();
                }

            bool addTags = await tagRepo.AddItemTagHandler(reqAddTagHandler);
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
        public async Task<IActionResult> RemoveItemTag([FromBody]ReqId reqId)
        {

            if (reqId == null)
            {
                return BadRequest();
            }

            bool getCollectTags = await tagRepo.RemoveItemTagJoin( reqId);
            if (!getCollectTags)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
