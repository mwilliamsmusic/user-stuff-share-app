﻿using ssa_database.Models.Collect_Models;


namespace user_stuff_share_app.Dtos.Collect.Collect_Dtos.Get_Collect

{
    public class ResGetCollect
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string CollectForm { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
    }
}