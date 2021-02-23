using System.Net;
using Newtonsoft.Json;

namespace TTWeb.BusinessLogic.Models
{
    public class ErrorResponseModel
    {
        public ErrorResponseModel(HttpStatusCode status, string message = null, string stack = null)
        {
            Status = status;
            Message = message;
            Stack = stack;
        }

        [JsonProperty("status")] public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;

        [JsonProperty("code")] public int Code => (int) Status;

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("stack", NullValueHandling = NullValueHandling.Ignore)]
        public string Stack { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}