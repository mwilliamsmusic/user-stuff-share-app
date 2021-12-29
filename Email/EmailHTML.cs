using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Email_Dtos;

namespace user_stuff_share_app.Email
{
    public class EmailHTML
    {

        private string HTMLPasswordReset1 = @"
<html lang=""en"">
    <head>    
        <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
<link rel=""preconnect"" href=""https://fonts.googleapis.com"">
<link rel = ""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
<link href = ""https://fonts.googleapis.com/css2?family=Permanent+Marker&display=swap"" rel=""stylesheet"">
        <title>
            Validation Confirmation
        </title>
        <style type=""text/css"">
@import url(""https://fonts.googleapis.com/css2?family=Permanent+Marker&display=swap"");
HTML{background-color: rgba(244, 43, 75, 1); font-family: Arial, Helvetica, sans-serif;  height: ""100vh"";
  min-height: ""100vh""; color:white; }

.center-content{ width: 100%; color:white; background-color: rgba(244, 43, 75, 1); margin:10px; }
            .logo{ font-family: ""Permanent Marker"", cursive; color:white; }
        </style>
    </head>
    <body>
        <div class=""center-content"">
         <div class = ""logo""> <p>  
<h1>Stuff Share App</h1>
</p></div>
<br>

   <span>  <p>  <h3> Reset Passcode</h3></p></span>

<span><p>
<h2>

";
        private string HTMLPasswordReset2 = @"</h2></p></span>
 <br><span>
<a href=""https://www.stuffshareapp.com/reset"">Reset Password</a><span>
<br>
</div></body></html>";

        private int GeneratePasscode()
        {
            Random rnd = new Random();
            int number = rnd.Next(100000, 999999);
            return number;
        }
        public ResPasswordReset PasswordResetHTML()
        {
            ResPasswordReset res = new ResPasswordReset();
            res.Passcode = GeneratePasscode();
            res.BodyHTML = $"{HTMLPasswordReset1}{res.Passcode.ToString()}{HTMLPasswordReset2}";
            return res;
        }
    }
}
