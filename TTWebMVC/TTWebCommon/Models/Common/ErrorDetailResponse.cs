using System.Collections.Generic;
using Newtonsoft.Json;

namespace TTWebCommon.Models.Common
{
    public class ErrorDetailResponse
    {
        public ErrorDetailResponse()
        {
        }

        [JsonProperty("status_code")] public int StatusCode { get; set; } = 500;

        [JsonIgnore] public ErrorCode ErrorCodeValue { get; set; }

        [JsonProperty("error_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode => ErrorCodeValue != Models.ErrorCode.INTERNAL_ERROR ? ErrorCodeValue.ToString() : null;

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Details { get; set; } = new Dictionary<string, string>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}