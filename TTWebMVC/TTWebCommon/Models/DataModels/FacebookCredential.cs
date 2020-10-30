using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TTWebCommon.Models
{
    public class FacebookCredential
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; } = 0;

        [Required]
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty("appuser_id", NullValueHandling = NullValueHandling.Ignore)]
        public int AppUserId { get; set; }
    }
}
