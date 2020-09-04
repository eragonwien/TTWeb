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

        [JsonIgnore]
        public ErrorCode ErrorCodeValue { get; set; }

        [JsonProperty("error_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode
        {
            get
            {
                return ErrorCodeValue != Models.ErrorCode.INTERNAL_ERROR ? ErrorCodeValue.ToString() : null;
            }
        }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
        public string Detail { get; set; }

        public ErrorDetailResponse()
        {
            
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
