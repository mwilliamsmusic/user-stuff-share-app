using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ssa_database.Models.Flag_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Repository_Interfaces.IFlag_Repository;

namespace user_stuff_share_app.Controllers.Flag_Controllers
{
    [Authorize]
    [Route("user/flag/item")]
    [ApiController]
    public class ItemFlagController : ControllerBase
    {

        private readonly UserInfo _userInfo;
        private readonly IItemFlagRepository _flagRepository;
        public ItemFlagController(IItemFlagRepository flagRepository, UserInfo userInfo)
        {
            _userInfo = userInfo;
            _flagRepository = flagRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlag([FromBody] ItemFlag itemFlag)
        {
            itemFlag.UserId = _userInfo.IdClaim(User);
            bool created = await _flagRepository.CreateItemFlag(itemFlag);

            if (created)
            {
                return NoContent();
            }
            return StatusCode(500);
        }
    }

}

