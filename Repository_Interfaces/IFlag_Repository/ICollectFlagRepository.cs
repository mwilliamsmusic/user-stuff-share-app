using ssa_database.Models.Flag_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Repository_Interfaces.IFlag_Repository
{
    public interface ICollectFlagRepository
    {
        Task<bool> CreateCollectFlag(CollectFlag flag);
    }
}
