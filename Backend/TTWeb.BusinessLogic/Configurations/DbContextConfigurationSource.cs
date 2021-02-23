using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TTWeb.BusinessLogic.Configurations
{
    public class DbContextConfigurationSource : IConfigurationSource
    {
        private readonly Action<DbContextOptionsBuilder> _builderAction;

        public DbContextConfigurationSource(Action<DbContextOptionsBuilder> builderAction)
        {
            _builderAction = builderAction;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbContextConfigurationProvider(_builderAction);
        }
    }
}
