using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Follow_Dtos
{
    public class ReqFollowUser
    {
        public long UserId { get; set; }
        public long FollowUserId { get; set; }
    }                
}
