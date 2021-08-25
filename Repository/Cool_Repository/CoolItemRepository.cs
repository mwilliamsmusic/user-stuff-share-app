using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ssa_database.Models.Cool_Models;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Create_Cool_Item;
using user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Create_Cool_Item_Join;
using user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Update_Cool_Item;
using user_stuff_share_app.Repository_Interfaces.ICool_Repository;

namespace user_stuff_share_app.Repository.Cool_Repository
{
    public class CoolItemRepository: ICoolItemRepository
    {
        private readonly ApplicationDbContext _dbJoin;
        private readonly ApplicationDbContext _dbCool;
        private readonly IMapper _mapper;

        public CoolItemRepository(ApplicationDbContext dbCool, ApplicationDbContext dbJoin, IMapper mapper)
        {
            _dbJoin = dbJoin;
            _dbCool = dbCool;
            _mapper = mapper;
        }

        public async Task<ResId> CreateCoolItem(ReqCreateCoolItem reqCreateCool)
        {
            CoolItem cool = _mapper.Map<CoolItem>(reqCreateCool);
            _dbCool.CoolItem.Add(cool);
            await SaveCoolItem();
            ResId coolId = new ResId() { Id = cool.Id};
            

            return coolId;
        }

        public async Task<bool> UpdateValueCool(ReqUpdateCoolItem reqUpdateCool)
        {
            CoolItem cool = await _dbCool.CoolItem.AsNoTracking().FirstOrDefaultAsync(c => c.Id == reqUpdateCool.Id);
            if (cool != null)
            {
                cool.Value++;
                _dbCool.Update(cool);
                return await SaveCoolItem();
            }
            return false;

        }

        public async Task<bool> AddCoolUser(ReqCreateCoolItemJoin reqCreateCoolJoin)
        {
            CoolItemJoin coolJoin = _mapper.Map<CoolItemJoin>(reqCreateCoolJoin);
            _dbJoin.CoolItemJoin.Add(coolJoin);
            return await SaveCoolJoin();

        }

        public async Task<bool> CoolUserExists(long userId)
        {
            return await _dbJoin.CoolCollectJoin.AsNoTracking().AnyAsync(c => c.UserId == userId);
        }


        private async Task<bool> SaveCoolItem()
        {
            return await _dbCool.SaveChangesAsync() >= 0 ? true : false; ;
        }


        private async Task<bool> SaveCoolJoin()
        {
            return await _dbJoin.SaveChangesAsync() >= 0 ? true : false; ;
        }
    }
}
