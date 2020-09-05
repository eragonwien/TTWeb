using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TTWebCommon.Models
{
    public class AppUser
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; } = 0;

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstname { get; set; }

        [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
        public string Lastname { get; set; }

        [JsonProperty("disabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool Disabled { get; set; }

        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty("facebook_credentials", NullValueHandling = NullValueHandling.Ignore)]
        public List<FacebookCredential> FacebookCredentials { get; set; } = new List<FacebookCredential>();

        [JsonProperty("role_id", NullValueHandling = NullValueHandling.Ignore)]
        public int RoleId { get; set; }

        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public UserRole Role { get; set; } = UserRole.Standard;

        public static AppUser Empty = new AppUser();
    }
}
