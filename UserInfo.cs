using System;
using System.Linq;
using System.Security.Claims;

namespace user_stuff_share_app
{
    public class UserInfo
    {
        public long IdClaim(ClaimsPrincipal User) {
            return  Int64.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());
        }
        public string UsernameClaim(ClaimsPrincipal Username)
        {
            return Username.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
        }
    }
}
