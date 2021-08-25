using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Create_Cool_Item;
using user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Create_Cool_Item_Join;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Create_Item;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Get_Item;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Item_Form;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Update_Item;
using user_stuff_share_app.Repository_Interfaces.ICollect_Repository;
using user_stuff_share_app.Repository_Interfaces.ICool_Repository;

namespace user_stuff_share_app.Controllers.Collect_Controllers
{
    [Route("user/item/collect")]
    [ApiController]
    [Authorize]
  
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepo;
        private readonly UserInfo _userInfo;
        private readonly ICoolItemRepository _coolItemRepo;

        public ItemController(IItemRepository itemRepo, ICoolItemRepository coolItemRepo, UserInfo userInfo)
        {
            _itemRepo = itemRepo;
            _userInfo = userInfo;
            _coolItemRepo = coolItemRepo;
        }


        // Create item in a collection
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ReqCreateItem reqCreateItem)
        {
            if (reqCreateItem == null)
            {
                return BadRequest(ModelState);
            }
            reqCreateItem.UserId = _userInfo.IdClaim(User);
            ResId createdItem = await _itemRepo.CreateItem(reqCreateItem);
            if (createdItem == null)
            {
                ModelState.AddModelError("", $"Something went wrong saving collection item!");
                return StatusCode(500, ModelState);
            }

            ReqCreateCoolItem reqCreateCool = new ReqCreateCoolItem() { ItemId = createdItem.Id, Value = 0 };
            ResId createCool = await _coolItemRepo.CreateCoolItem(reqCreateCool);
            if (createCool == null)
            {
                
                return StatusCode(500);
            }


            ReqCreateCoolItemJoin reqCreateCoolJoin = new ReqCreateCoolItemJoin() { UserId = reqCreateItem.UserId, CoolItemId = createCool.Id, ItemId=createdItem.Id };
            bool coolJoin = await _coolItemRepo.AddCoolUser(reqCreateCoolJoin);
            if (!coolJoin)
            {
                return StatusCode(500);
            }



            return Ok(createdItem);
          
        }

        [HttpPost("one")]
        public async Task<IActionResult> GetItem([FromBody] ReqId reqId)
        {
            if (reqId == null)
            {
                return NotFound();
            }
           
            ResGetItem resGet = await _itemRepo.GetItem(reqId);
            if (resGet == null)
            {
                ModelState.AddModelError("", $"Something went wrong saving collection item!");
                return StatusCode(500, ModelState);
            }
            return Ok(resGet);
        }


        [HttpPost("all")]
        public async Task<IActionResult> GetAllItems([FromBody] ReqCollectId reqCollectId)
        {
     
                IEnumerable<ResGetItem> items = await _itemRepo.GetAllItems(reqCollectId);
                if (items == null)
                {
                    return NotFound();
                }
            return Ok(items);
        }

        [HttpPatch("ui")]
        public async Task<IActionResult> UpdateItem([FromBody] ReqUpdateItem reqUpdate)
        {
            reqUpdate.UserId = _userInfo.IdClaim(User);
            if (reqUpdate == null)
                {
                    return NotFound();
                }

                bool updatedItem = await _itemRepo.UpdateItemCollect(reqUpdate);
                if (!updatedItem)
                {
                    ModelState.AddModelError("", $"Something went wrong updating items");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
        }

        [HttpDelete("di")]
        public async Task<IActionResult> DeleteItem([FromBody] ReqId reqId)
        {
            if (reqId == null)
            {
                return NotFound();
            }


           bool deletedItem = await _itemRepo.DeleteItemCollect(reqId);

                if (!deletedItem)
                {
                    ModelState.AddModelError("", $"Something went wrong deleting collection item!");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
         }


        // Update Item Form
        [HttpPatch("uif")]
        public async Task<IActionResult> UpdateForm([FromBody] ReqUpdateIF reqUpdate)
        {
            if (reqUpdate == null)
            {
                return NotFound();
            }


            bool updatedForm = await _itemRepo.UpdateItemForm(reqUpdate);
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