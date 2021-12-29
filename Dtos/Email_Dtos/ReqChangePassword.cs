using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Email_Dtos
{
    public class ReqChangePassword
    {
        public string Email { get; set; }
        public string Pass { get; set; }
    }
}
