using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Item.Tag_Item_Dtos
{
    public class ReqRemoveItemTag
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long UserId { get; set; }
    }
}
