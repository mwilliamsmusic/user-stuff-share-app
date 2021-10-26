using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Email
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
