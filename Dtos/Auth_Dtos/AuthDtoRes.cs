using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Auth_Dtos
{
    public class AuthDtoRes
    {    
        public long Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
