using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Join
{
    public class ReqCreateCoolJoin
    {
        public long UserId { get; set; }
        public long CoolCollectId { get; set; }
        public long CollectId { get; set; }
    }
}
