using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ssa_database.Models.Bookmark_Models;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Repository_Interfaces.IBookmark_Repository;

namespace user_stuff_share_app.Controllers.Bookmark_Controllers
{
    [Authorize]
    [Route("user/bookmark/collect/")]
    [ApiController]
    public class BookmarkCollectController: ControllerBase
    {
        private readonly IBookmarkCollectRepository _bookmarkRepo;
     
        private readonly UserInfo _userInfo;
        public BookmarkCollectController(IBookmarkCollectRepository bookmarkRepo, ApplicationDbContext db, UserInfo userInfo)
        {
            _bookmarkRepo = bookmarkRepo;
        
            _userInfo = userInfo;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBookmark(BookmarkCollect bookmarkCollect) {
            if (bookmarkCollect == null)
            {
                return BadRequest();
            }

            bookmarkCollect.UserId = _userInfo.IdClaim(User);
            bool createdItem = await _bookmarkRepo.AddBookmark(bookmarkCollect);

            if (!createdItem)
            {
                ModelState.AddModelError("", $"Something went wrong saving collection item!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBookmark(ReqIdUserId reqIdUserId) {
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
