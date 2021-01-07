using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TTWeb.BusinessLogic.Services
{
    public class ConfigurationEntryService : IConfigurationEntryService
    {
        public Task<Dictionary<string, string>> Get(string key)
        {
            throw new NotImplementedException();
        }
    }
}