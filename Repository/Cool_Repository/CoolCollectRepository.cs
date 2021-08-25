using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ssa_database.Models.Cool_Models;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Collect;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Join;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Update_Cool_Collect;
using user_stuff_share_app.Repository_Interfaces.ICool_Repository;

namespace user_stuff_share_app.Repository.Cool_Repository
{
    public class CoolCollectRepository: ICoolCollectRepository
    {

        private readonly ApplicationDbContext _dbJoin;
        private readonly ApplicationDbContext _dbCool;
        private readonly IMapper _mapper;

        public CoolCollectRepository(ApplicationDbContext dbCool, ApplicationDbContext dbJoin, IMapper mapper)
        {
            _dbJoin = dbJoin;
            _dbCool = dbCool;
            _mapper = mapper;
        }



        public async Task<ResId> CreateCoolCollect(ReqCreateCoolCollect reqCreateCool)
        {
            CoolCollect cool = _mapper.Map<CoolCollect>(reqCreateCool);
            _dbCool.CoolCollect.Add(cool);
            await SaveCoolCollect();
            ResId coolId = new() { Id = cool.Id };
            return coolId;
        }

        public async Task<bool> UpdateValueCoolCollect(ReqUpdateCoolCollect reqUpdateCool)
        {
            CoolCollect cool = await _dbCool.CoolCollect.AsNoTracking().FirstOrDefaultAsync(c => c.Id == reqUpdateCool.Id && c.CollectId == reqUpdateCool.CollectId);
            if (cool != null)
            {
                cool.Value = cool.Value + 1;
                _dbCool.Update(cool);
                return await SaveCoolCollect();
            }
            return false;
        }

        public async Task<bool> AddCoolUser(ReqCreateCoolJoin reqCreateCoolJoin)
        {
            CoolCollectJoin coolJoin = _mapper.Map<CoolCollectJoin>(reqCreateCoolJoin);
            _dbJoin.CoolCollectJoin.Add(coolJoin);
            return await SaveCoolJoin();

        }

        public async Task<bool> CoolUserExists(long userId)
        {
            return await _dbJoin.CoolCollectJoin.AsNoTracking().AnyAsync(c => c.UserId == userId);
        }


        public async Task<bool> SaveCoolCollect()
        {
            return await _dbCool.SaveChangesAsync() >= 0 ? true : false; ;
        }


        public async Task<bool> SaveCoolJoin()
        {
            return await _dbJoin.SaveChangesAsync() >= 0 ? true : false; ;
        }
    }
}

