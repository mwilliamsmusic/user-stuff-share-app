using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Email_Dtos
{
    public class ReqWritePasscode
    {
        public string Email {get; set;}
        public int Passcode { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
