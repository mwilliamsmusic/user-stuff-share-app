using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Update_Cool_Item
{
    public class ReqUpdateCoolItem
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ItemId { get; set; }
    }
}
