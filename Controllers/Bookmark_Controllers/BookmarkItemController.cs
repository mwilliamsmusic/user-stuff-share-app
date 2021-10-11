using Microsoft.AspNetCore.Mvc;
using ssa_database.Models.Bookmark_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Repository_Interfaces.IBookmark_Repository;

namespace user_stuff_share_app.Controllers.Bookmark_Controllers
{
    [Route("user/bookmark/item/")]
    [ApiController]
    public class BookmarkItemController:ControllerBase
    {
        private readonly IBookmarkItemRepository _bookmarkRepo;

        private readonly UserInfo _userInfo;
        public BookmarkItemController(IBookmarkItemRepository bookmarkRepo,  UserInfo userInfo)
        {
            _bookmarkRepo = bookmarkRepo;
            _userInfo = userInfo;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBookmark(BookmarkItem bookmarkItem)
        {
            if (bookmarkItem == null)
            {
                return BadRequest();
            }

            bookmarkItem.UserId = _userInfo.IdClaim(User);
            bool createdItem = await _bookmarkRepo.AddBookmark(bookmarkItem);

            if (!createdItem)
            {
                ModelState.AddModelError("", $"Something went wrong saving collection item!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBookmark(ReqIdUserId reqIdUserId)
        {
            if (reqIdUserId == null)
            {
                return NotFound();
            }

            reqIdUserId.UserId = _userInfo.IdClaim(User);

            bool deletedItem = await _bookmarkRepo.RemoveBookmark(reqIdUserId);

            if (!deletedItem)
            {
                ModelState.AddModelError("", $"Something went wrong deleting collection item!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
