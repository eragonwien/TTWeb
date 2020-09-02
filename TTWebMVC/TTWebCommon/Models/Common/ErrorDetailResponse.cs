using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebCommon.Models.Common
{
    public class ErrorDetailResponse
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }
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
