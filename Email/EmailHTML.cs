using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Email
{
    public class EmailHTML
    {
       



      private string HTMLBody1 = @"
<html lang=""en"">
    <head>    
        <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
        <title>
            Upcoming topics
        </title>
        <style type=""text/css"">


            HTML{background-color: rgba(244, 43, 75, 1);}

.center-content{ display: flex; justify-content: center; width: 100%;}
            
        </style>
    </head>
    <body>
        <table class=""courses-table"">
            <thead>
                <tr>
                    <th class=""green"">Topic</th>
                    <th class=""green"">Est. # of posts</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class=""description"">Using a Windows service in your project</td>
                    <td>5</td>

";
        private string HTMLBody2 = @"                </tr>
                <tr>
                    <td class=""description"">More RabbitMQ in .NET</td>
                    <td>{{{meh}}}</td>
                </tr>
            </tbody>
        </table>
    </body>
</html>";

        private int num()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 5000);
            return number;
        }
        public string PasswordResetHTML()
        {
            return $"{ HTMLBody1}{num()}{HTMLBody2}";
        }
    }
}
