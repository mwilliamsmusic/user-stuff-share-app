using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ssa_database.Models.Email_Models;
using ssa_database.Models.User_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Email_Dtos;

namespace user_stuff_share_app.Email
{
    using BCrypt = BCrypt.Net.BCrypt;
    public class EmailRpository : IEmailRepository
    {
        private readonly AppSettings _appSettings;
        private MailKitEmailSenderOptions Options;
        private readonly ApplicationDbContext _dbUser;
        private readonly ApplicationDbContext _dbReset;

        public EmailRpository(IOptions<MailKitEmailSenderOptions> options, ApplicationDbContext dbUser, ApplicationDbContext dbReset, IOptions<AppSettings> appSettings)
        {
            Options = options.Value;
            _dbUser = dbUser;
            _dbReset = dbReset;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> WritePasscodeDB(ReqWritePasscode reqWritePasscode)
        {
            return await SaveReset();
        }

        public async Task<bool> CheckPasscodeEmail(ReqWritePasscode reqWritePasscode)
        {
            Reset resReset = await _dbReset.Reset.AsNoTracking().FirstOrDefaultAsync(r=>r.Email==reqWritePasscode.Email && r.Passcode == reqWritePasscode.Passcode);
    
            int checkTime = DateTimeOffset.Compare(resReset.Created,DateTimeOffset.UtcNow );

            if (checkTime <= 0)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> CheckEmailExist(ReqEmail reqEmail)
        {
            return await _dbUser.User.AsNoTracking().AnyAsync(c => c.Email == reqEmail.Email);
        }

        public Task SendEmailAsync(string email, string subject, ResPasswordReset html)
        {
            return Execute(email, subject, html);
        }

        private Task Execute(string to, string subject, ResPasswordReset html)
        {
            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(Options.Sender_EMail);
            if (!string.IsNullOrEmpty(Options.Sender_Name))
                email.Sender.Name = Options.Sender_Name;
            email.From.Add(email.Sender);
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html.BodyHTML };


            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(Options.Host_Address, Options.Host_Port, Options.Host_SecureSocketOptions);
                smtp.Authenticate(Options.Host_Username, Options.Host_Password);
                smtp.Send(email);
                smtp.Disconnect(true);
                smtp.Disconnect(true);
            }

            return Task.FromResult(true);
        }


        public async Task<bool> UpdatePassword(ReqChangePassword reqChangePassword)
        {
            User user = await _dbUser.User.FirstOrDefaultAsync(r => r.Email == reqChangePassword.Email);
            user.Password = Crypt(reqChangePassword.Pass);
            return await  SaveUser();
        }

        public async Task<bool> SaveUser()
        {
            return await _dbUser.SaveChangesAsync() >= 0 ? true : false; ;
        }
    

    public async Task<bool> SaveReset()
    {
        return await _dbReset.SaveChangesAsync() >= 0 ? true : false; ;
    }


        private string Crypt(string password)
        {
            byte[] spice = Encoding.UTF8.GetBytes(_appSettings.Spice);
            string spiceString = password + Encoding.UTF8.GetString(spice);
            string spicePassword = BCrypt.EnhancedHashPassword(spiceString);
            return spicePassword;
        }



    }
}
