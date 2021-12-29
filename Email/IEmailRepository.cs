using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Email_Dtos;

namespace user_stuff_share_app.Email
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(string email, string subject, ResPasswordReset html);
        Task<bool> CheckEmailExist(ReqEmail reqEmail);
        Task<bool> WritePasscodeDB(ReqWritePasscode reqWritePasscode);
        Task<bool> CheckPasscodeEmail(ReqWritePasscode reqWritePasscode);
        Task<bool> UpdatePassword(ReqChangePassword reqChangePassword);
    }
}
