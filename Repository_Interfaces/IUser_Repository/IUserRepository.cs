using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Auth_Dtos;
using user_stuff_share_app.Dtos.Reg_Dtos;

namespace user_stuff_share_app.Repository_Interfaces.IUser_Repository
{
    public interface IUserRepository
    {
        Task<AuthDtoRes> Authentication(AuthDtoReq authDtoReq);
        Task<bool> Register(RegDtoReq regDtoReq);
        Task<bool> IsUniqueUser(string username);
    }
}
