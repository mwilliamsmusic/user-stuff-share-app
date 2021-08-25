using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Update_Cool_Collect
{
    public class ReqUpdateCoolCollect
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CollectId { get; set; }
    }
}
