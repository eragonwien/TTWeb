using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWeb.Data.Models
{
    public class UserPermission
    {
        public int Id { get; set; }
        public UserPermissionEnum Value { get; set; }
        public string Description { get; set; }
        public ICollection<LoginUserPermissionMapping> LoginUserPermissionMappings { get; set; }
    }

    public enum UserPermissionEnum
    {
        DEFAULT
    }
}
