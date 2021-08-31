using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Auth_Dtos;
using user_stuff_share_app.Dtos.Reg_Dtos;
using user_stuff_share_app.Repository_Interfaces.IUser_Repository;
using user_stuff_share_app.Status_Messages;

namespace user_stuff_share_app.Controllers
{
    [Route("/user")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly StatusMessages _statusMessages;
        public UserController(IUserRepository userRepo, StatusMessages statusMessages )
        {
            _userRepo = userRepo;
            _statusMessages = statusMessages;
        }
   
         [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthDtoReq authDtoReq)
        {
            var user = await _userRepo.Authentication(authDtoReq);

            if (user == null)
            {
                return BadRequest(_statusMessages.IncorrectLogin());
            }
            Response.Cookies.Append("X-Access-Token", user.Token, new CookieOptions() { /*HttpOnly = true,*/
                SameSite = SameSiteMode.None,
                Domain = null,
                Secure = true, IsEssential = true,
                Expires= DateTime.Now.AddDays(20d) });

            return Ok(user.Username);
        }

         [AllowAnonymous]
        [HttpPost("reg")]
        public async Task<IActionResult> RegisterUser([FromBody] RegDtoReq regDtoReq)
        {
            bool ifUsernameUnique = await _userRepo.IsUniqueUser(regDtoReq.Username);

            if (!ifUsernameUnique)
            {
                return BadRequest( _statusMessages.UsernameExists());
            }
            bool user = await  _userRepo.Register(regDtoReq);
            if (user == false)
            {
                return BadRequest(_statusMessages.RegisterError());
            }

            return NoContent();
        }
    }
}
