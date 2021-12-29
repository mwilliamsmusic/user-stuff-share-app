using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ssa_database.Models.Collect_Models;
using ssa_database.Models.Flag_Models;
using ssa_database.Models.User_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Repository_Interfaces.IFlag_Repository;

namespace user_stuff_share_app.Repository.Flag_Repository
{
    [Authorize]
    [Route("user/flag/item")]
    [ApiController]
    public class CollectFlagRepository: ICollectFlagRepository
    {
        private readonly ApplicationDbContext _dbFlag;
        private readonly ApplicationDbContext _dbCollect;
      
        private readonly IMapper _mapper;
        public CollectFlagRepository(ApplicationDbContext dbFlag, ApplicationDbContext dbCollect, IMapper mapper)
        {
            _dbFlag = dbFlag;
            _dbCollect = dbCollect;
            _mapper = mapper;
      
        }
        /*
        public async Task<bool> UpdateStatus(string url)
        {

            CollectFlag flag = await _db.CollectFlag.FirstOrDefaultAsync(c => c.Url == url);
            return await Save();
        }
        */
        public async Task<bool> CreateCollectFlag(CollectFlag collectFlag)
        {
    
           await  _dbFlag.CollectFlag.AddAsync(collectFlag);
            bool status = await CheckStatus(collectFlag);
            
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _dbFlag.SaveChangesAsync() >= 0 ? true : false; ;
        }

        public async Task<bool> SaveCollect()
        {
            return await _dbCollect.SaveChangesAsync() >= 0 ? true : false; ;
        }

        private async Task<bool> CheckStatus(CollectFlag collectFlag)
        {
            if (collectFlag.Rating == "mature")
            {

                int count = _dbFlag.CollectFlag.Count(t => t.CollectId == collectFlag.CollectId && t.Rating == "mature");
                if (count > 5)
                {
                    Collect collect = await _dbCollect.Collect.FindAsync(collectFlag.CollectId);
                    collect.Status = "mature";
                    return await SaveCollect();
                }
            }
             if (collectFlag.Rating == "illegal")
            {
                int illegalCount = _dbFlag.CollectFlag.Count(t => t.CollectId == collectFlag.CollectId && t.Rating == "illegal");
                if (illegalCount > 2)
                {
                    Collect collect = await _dbCollect.Collect.FindAsync(collectFlag.CollectId);
                    collect.Status = "block";
                    return await SaveCollect();
                }

            }
            return false;
        } 

    }
}
