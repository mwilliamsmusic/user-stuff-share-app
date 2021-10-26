using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> PasswordReset()
        {
            EmailHTML _emailHTML = new EmailHTML();
            await _emailRepository.SendEmailAsync("fasmusic@gmail.com", "but", _emailHTML.PasswordResetHTML());
            return Ok();
        }
    }
}
