using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ssa_database.Models.User_Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Auth_Dtos;
using user_stuff_share_app.Dtos.Reg_Dtos;
using user_stuff_share_app.Repository_Interfaces.IUser_Repository;

namespace user_stuff_share_app.Repository.User_Repository
{
    using BCrypt = BCrypt.Net.BCrypt;
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;

        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appsettings)
        {
            _db = db;
            _appSettings = appsettings.Value;
        }
        
     
        public async  Task<AuthDtoRes> Authentication(AuthDtoReq authDtoReq)
        {
           string spice = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(_appSettings.Spice));
            string spiceString = authDtoReq.Password + spice;
            

            var user = await _db.User.SingleOrDefaultAsync(x => x.Username == authDtoReq.Username);
          

            // Generate JWT if found
            if (user != null)
            {
                
                bool validatePassword = BCrypt.EnhancedVerify(spiceString, user.Password);

                if (validatePassword)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Tok);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]{
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username.ToString())
                    }),
                        Expires = DateTime.UtcNow.AddDays(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                    };

                    var tok = Encoding.UTF8.GetBytes(_appSettings.Tok);
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    AuthDtoRes response = new AuthDtoRes
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Token = tokenHandler.WriteToken(token)
                    };

                    return response;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        //MAKE private
        public async Task<bool> IsUniqueUser(string username)
        {
            var user = await _db.User.SingleOrDefaultAsync(x => x.Username == username);
            if (user == null)
                return true;
            return false;
        }

        public async Task<bool> Register(RegDtoReq regDtoReq)
        {
           string spice = Crypt(regDtoReq.Password);

            try
            {
                User request = new User()
                {
                    Username = regDtoReq.Username,
                    Password = spice,
                    Email = regDtoReq.Email
                };
                await _db.User.AddAsync(request);
                await _db.SaveChangesAsync();
             
                return true;
            }
            catch(Exception e)
            {
                return false;
            }       
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
