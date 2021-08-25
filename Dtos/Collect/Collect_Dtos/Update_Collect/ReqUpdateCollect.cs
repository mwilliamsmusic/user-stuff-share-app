
using ssa_database.Models.Collect_Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace user_stuff_share_app.Dtos.Collect.Collect_Dtos.Update_Collect
{
    public class ReqUpdateCollect
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string CollectForm { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
    }
}
