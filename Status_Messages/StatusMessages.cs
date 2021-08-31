using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Status_Messages
{
    public class StatusMessages
    {
        // User
        public ResStatusMessage UsernameExists()
        {
            return  new ResStatusMessage() { Message = "Username already exists." };
        }
        public ResStatusMessage IncorrectLogin()
        {
            return new ResStatusMessage() { Message = " Username or password is incorrect." };
        }
        public ResStatusMessage RegisterError()
        {
            return new ResStatusMessage() { Message = "Error while registering. Please try again." };
        }


        // Collect

        // Item

        // Tag
        public ResStatusMessage TagJoinCheck()
        {
            return new ResStatusMessage() { Message = "Tag already exists." };
        }
    }
}
