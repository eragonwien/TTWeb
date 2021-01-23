﻿using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class LoginUser : IHasIdEntity
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<LoginUserPermissionMapping> LoginUserPermissionMappings { get; set; }

        public ICollection<FacebookUser> OwnedFacebookUsers { get; set; }

        public ICollection<Schedule> OwnedSchedules { get; set; }
        public int Id { get; set; }
    }
}