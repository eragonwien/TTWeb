using System;
using System.Collections.Generic;
using System.Text;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models
{
    public class LoginUserModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
