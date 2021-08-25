using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Create_Cool_Item_Join
{
    public class ReqCreateCoolItemJoin
    {
        public long UserId { get; set; }
        public long CoolItemId { get; set; }
        public long ItemId { get; set; }
    }
}
