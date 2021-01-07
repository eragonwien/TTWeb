using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TTWeb.Data.Database;

namespace TTWeb.BusinessLogic.Configurations
{
    public class DbContextConfigurationProvider : ConfigurationProvider
    {
        private readonly Action<DbContextOptionsBuilder> _builderAction;

        public DbContextConfigurationProvider(Action<DbContextOptionsBuilder> builderAction)
        {
            _builderAction = builderAction;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<TTWebContext>();

            _builderAction?.Invoke(builder);

            using var context = new TTWebContext(builder.Options);
            context.Database.Migrate();

            Data = context.ConfigurationEntries.ToDictionary(c => c.Key, c => c.Value);
        }
    }
}