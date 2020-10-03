using System;
using System.Collections.Generic;
using System.Text;

namespace TTWeb.Data.Models
{
    public class LoginUserPermissionMapping
    {
        public int LoginUserId { get; set; }
        public LoginUser LoginUser { get; set; }
        public int UserPermissionId { get; set; }
        public UserPermission UserPermission { get; set; }
    }
}
