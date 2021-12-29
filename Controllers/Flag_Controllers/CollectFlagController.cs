
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ssa_database.Models.Flag_Models;
using System.Threading.Tasks;
using user_stuff_share_app.Repository_Interfaces.IFlag_Repository;

namespace user_stuff_share_app.Controllers.Flag_Controllers
{

    [Route("user/flag/collect")]
    [Authorize]
    [ApiController]
    public class CollectFlagController : ControllerBase
    {
        private readonly UserInfo _userInfo;
        private readonly ICollectFlagRepository _flagRepository;
        public CollectFlagController(ICollectFlagRepository flagRepository, UserInfo userInfo)
        {
            _userInfo = userInfo;
            _flagRepository = flagRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlag([FromBody] CollectFlag collectFlag)
        {
            collectFlag.UserId = _userInfo.IdClaim(User);
            bool created = await _flagRepository.CreateCollectFlag(collectFlag);

            if (created)
            {
                return NoContent();
            }
            return StatusCode(500);
        }
    }
}
