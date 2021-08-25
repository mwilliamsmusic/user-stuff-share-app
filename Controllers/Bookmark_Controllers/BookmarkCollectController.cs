using Microsoft.AspNetCore.Mvc;
using ssa_database.Models.Bookmark_Models;
using System.Threading.Tasks;
using user_stuff_share_app.Repository_Interfaces.IBookmark_Repository;

namespace user_stuff_share_app.Controllers.Bookmark_Controllers
{
    [Route("end/user/bookmark/collect/")]
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

        [HttpPost]
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
        public async Task<IActionResult> DeleteBookmark(BookmarkCollect bookmarkCollect) {
            if (bookmarkCollect == null)
            {
                return NotFound();
            }

            bookmarkCollect.UserId = _userInfo.IdClaim(User);

            bool deletedItem = await _bookmarkRepo.RemoveBookmark(bookmarkCollect);

            if (!deletedItem)
            {
                ModelState.AddModelError("", $"Something went wrong deleting collection item!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
