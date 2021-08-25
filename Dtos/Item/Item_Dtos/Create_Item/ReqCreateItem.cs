using ssa_database.Models.Collect_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Item.Item_Dtos.Create_Item
{
    public class ReqCreateItem
    {
        public long UserId { get; set; }
        public long CollectId { get; set; }
        public string Title { get; set; }
        public string ItemForm { get; set; }
    }
}

