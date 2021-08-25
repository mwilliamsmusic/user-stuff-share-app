using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Follow_Dtos;
using user_stuff_share_app.Repository_Interfaces.IFollow_Repository;

namespace user_stuff_share_app.Controllers.Follow_Controllers
{
    [Route("end/user/follow/")]
    [Authorize]
    [ApiController]
    public class FollowUserController : ControllerBase
    {
        private readonly IFollowUserRepository _followUserRepository;
        private readonly ApplicationDbContext _db;
        private readonly UserInfo _userInfo;
        public FollowUserController(ApplicationDbContext db, UserInfo userInfo, IFollowUserRepository followUserRepository)
        {
            _db = db;
            _userInfo = userInfo;
            _followUserRepository = followUserRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFollow([FromBody] ReqFollowUser reqFollowUser)
        {

            if (reqFollowUser == null)
            {
                return BadRequest(ModelState);
            }

            reqFollowUser.UserId = _userInfo.IdClaim(User);
            bool createdItem = await _followUserRepository.AddFollow(reqFollowUser);

            if (!createdItem)
            {
                ModelState.AddModelError("", $"Something went wrong saving collection item!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFollow([FromBody] ReqFollowUser reqFollowUser)
        {
            if (reqFollowUser == null)
            {
                return NotFound();
            }

            reqFollowUser.UserId = _userInfo.IdClaim(User);

            bool deletedItem = await _followUserRepository.RemoveFollow(reqFollowUser);

            if (!deletedItem)
            {
                ModelState.AddModelError("", $"Something went wrong deleting collection item!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
