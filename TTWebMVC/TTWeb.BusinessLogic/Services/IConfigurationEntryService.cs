using System.Collections.Generic;
using System.Threading.Tasks;

namespace TTWeb.BusinessLogic.Services
{
    public interface IConfigurationEntryService
    {
        Task<Dictionary<string, string>> Get(string key);
    }
}
