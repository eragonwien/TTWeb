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
        public string Detail { get; set; }

        public ErrorDetailResponse(int statusCode, string title = null, string detail = null)
        {
            StatusCode = statusCode;
            Title = title;
            Detail = detail;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
