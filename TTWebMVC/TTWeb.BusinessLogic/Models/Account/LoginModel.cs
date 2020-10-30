using System;
using System.Collections.Generic;
using System.Text;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class LoginModel
    {
        public string Provider { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
