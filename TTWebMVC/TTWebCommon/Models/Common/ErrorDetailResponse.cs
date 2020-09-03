using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebCommon.Models.Common
{
    public class ErrorDetailResponse
    {
        [JsonProperty("status_code")]
        public int StatusCode { get; set; } = 500;

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("stack_trace", NullValueHandling = NullValueHandling.Ignore)]
        public string StackTrace { get; set; }

        public ErrorDetailResponse()
        {
            
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
