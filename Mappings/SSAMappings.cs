using AutoMapper;
using ssa_database.Models.Collect_Models;
using ssa_database.Models.Cool_Models;
using ssa_database.Models.Tag_Models;
using ssa_database.Models.User_Models;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Create_Collect;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Get_Collect;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Collect;
using user_stuff_share_app.Dtos.Collect.Cool_Collect_Dtos.Create_Cool_Join;
using user_stuff_share_app.Dtos.Follow_Dtos;
using user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Create_Cool_Item;
using user_stuff_share_app.Dtos.Item.Cool_Item_Dtos.Create_Cool_Item_Join;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Create_Item;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Get_Item;

namespace user_stuff_share_app.Mappings
{
    public class SSAMappings: Profile
    {
        public SSAMappings()
        {
            // Collect
         //   CreateMap<Collect, ReqGetCollect>().ReverseMap();
            CreateMap<Collect, ResGetCollect>().ReverseMap();
            CreateMap<Collect, ReqCreateCollect>().ReverseMap();
           // CreateMap<Collect, ReqDeleteCollect>().ReverseMap();
         //   CreateMap<Collect, ResGetCollectByUsername>().ReverseMap();

            // Item
            CreateMap<Item, ResGetCollect>().ReverseMap();
            CreateMap<Item, ReqCreateItem>().ReverseMap();
            CreateMap<Item, ResGetItem>().ReverseMap();
          //  CreateMap<Item, ReqDeleteItem>().ReverseMap();
            

            // Cool
            CreateMap<CoolCollect, ReqCreateCoolCollect>().ReverseMap();
           
            CreateMap<CoolCollectJoin, ReqCreateCoolJoin>().ReverseMap();
            CreateMap<CoolItem, ReqCreateCoolItem>().ReverseMap();
            CreateMap<CoolItemJoin, ReqCreateCoolItemJoin>().ReverseMap();

            // Follow
            CreateMap<FollowUser, ReqFollowUser>().ReverseMap();

            // Tags
            CreateMap<TagCollectJoin, ResTagId>().ReverseMap();
            CreateMap<TagItemJoin, ResTagId>().ReverseMap();

        }
    }
}
