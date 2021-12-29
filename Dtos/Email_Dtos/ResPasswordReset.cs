using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Email_Dtos
{
    public class ResPasswordReset
    {
        public int Passcode { get; set; }
        public string BodyHTML { get; set; }
    }
}
