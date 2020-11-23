using System.IO;

namespace TTWeb.BusinessLogic.Services
{
    public class HelperService : IHelperService
    {
        public string GetRandomString(int length)
        {
            return Path.GetRandomFileName().Replace(".", "");
        }
    }
}