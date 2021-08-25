using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Item.Tag_Item_Dtos
{
    public class ReqAddTagItemHandler
    {
        public string TagId { get; set; }
        public long ItemId { get; set; }
    }
}
