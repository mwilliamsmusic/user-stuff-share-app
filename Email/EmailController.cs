using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Email_Dtos;

namespace user_stuff_share_app.Email
{
    [Route("email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;

        public EmailController(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;

        }

        [HttpPost("pr")]
        public async Task<IActionResult> PasswordReset([FromBody] ReqEmail reqEmail)
        {
            // Check to see if email exists
            bool emailCheck = await _emailRepository.CheckEmailExist(reqEmail);
            // If true 
            if (emailCheck)
            {
                EmailHTML _emailHTML = new EmailHTML();
                ResPasswordReset html= _emailHTML.PasswordResetHTML();
                await _emailRepository.SendEmailAsync(reqEmail.Email, "Validation Confirmation", html);
                ReqWritePasscode reqWrite = new() { Email = reqEmail.Email, Passcode = html.Passcode, Created = DateTimeOffset.UtcNow.AddMinutes(6) };
                bool resPasscode = await _emailRepository.WritePasscodeDB(reqWrite);
                if (resPasscode)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
            return NotFound();
        }

        [HttpPut("pc")]
        public async Task<IActionResult> PasswordChange([FromBody] ReqChangePassword reqChangePassword)
        {
            bool check = await _emailRepository.UpdatePassword(reqChangePassword);
            if (check)
            {
                return NoContent();
            }
            return StatusCode(500);
      
        }
    }
}
