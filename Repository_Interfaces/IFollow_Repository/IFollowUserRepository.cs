using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Follow_Dtos;

namespace user_stuff_share_app.Repository_Interfaces.IFollow_Repository
{
    public interface IFollowUserRepository
    {
        Task<bool> AddFollow(ReqFollowUser reqUserFollow);
        Task<bool> RemoveFollow(ReqFollowUser reqUserFollow);
    }
}
