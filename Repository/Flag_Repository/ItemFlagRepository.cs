using AutoMapper;
using ssa_database.Models.Collect_Models;
using ssa_database.Models.Flag_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Repository_Interfaces.IFlag_Repository;

namespace user_stuff_share_app.Repository.Flag_Repository
{
    public class ItemFlagRepository: IItemFlagRepository
    {
        private readonly ApplicationDbContext _dbFlag;
        private readonly ApplicationDbContext _dbItem;

        private readonly IMapper _mapper;
        public ItemFlagRepository(ApplicationDbContext dbFlag, ApplicationDbContext dbItem, IMapper mapper)
        {
            _dbFlag = dbFlag;
            _dbItem = dbItem;
            _mapper = mapper;

        }
        /*
        public async Task<bool> UpdateStatus(string url)
        {

            CollectFlag flag = await _db.CollectFlag.FirstOrDefaultAsync(c => c.Url == url);
            return await Save();
        }
        */
        public async Task<bool> CreateItemFlag(ItemFlag itemFlag)
        {

            await _dbFlag.ItemFlag.AddAsync(itemFlag);
            bool status = await CheckStatus(itemFlag);

            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _dbFlag.SaveChangesAsync() >= 0 ? true : false; ;
        }

        public async Task<bool> SaveItem()
        {
            return await _dbItem.SaveChangesAsync() >= 0 ? true : false; ;
        }

        private async Task<bool> CheckStatus(ItemFlag itemFlag)
        {
            if (itemFlag.Rating == "mature")
            {

                int count = _dbFlag.ItemFlag.Count(t => t.ItemId == itemFlag.ItemId && t.Rating == "mature");
                if (count > 5)
                {
                    Item item = await _dbItem.Item.FindAsync(itemFlag.ItemId);
                    item.Status = "mature";
                    return await SaveItem();
                }
            }
            if (itemFlag.Rating == "illegal")
            {
                int illegalCount = _dbFlag.ItemFlag.Count(t => t.ItemId == itemFlag.ItemId && t.Rating == "illegal");
                if (illegalCount > 2)
                {
                   Item item = await _dbItem.Item.FindAsync(itemFlag.ItemId);
                    item.Status = "block";
                    return await SaveItem();
                }

            }
            return false;
        }

    }
}
