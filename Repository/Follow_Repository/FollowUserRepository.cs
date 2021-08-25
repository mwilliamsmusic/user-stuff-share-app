using AutoMapper;
using ssa_database.Models.User_Models;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Follow_Dtos;
using user_stuff_share_app.Repository_Interfaces.IFollow_Repository;

namespace user_stuff_share_app.Repository.Follow_Repository
{
    public class FollowUserRepository:IFollowUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public FollowUserRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> AddFollow(ReqFollowUser reqUserFollow)
        {
            FollowUser addFollow = _mapper.Map<FollowUser>(reqUserFollow);
            await _db.FollowUser.AddAsync(addFollow);
            return await Save();
        }
        public async Task<bool> RemoveFollow(ReqFollowUser reqUserFollow)
        {
            FollowUser removeFollow = _mapper.Map<FollowUser>(reqUserFollow);
           _db.FollowUser.Remove(removeFollow);
            return await Save();
    }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0 ? true : false; ;
        }

    }
}
