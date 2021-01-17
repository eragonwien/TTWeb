using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TTWeb.BusinessLogic.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> LoadJsonAsync<T>(this HttpResponseMessage response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}