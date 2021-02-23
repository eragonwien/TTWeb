using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TTWeb.BusinessLogic.Configurations;

namespace TTWeb.BusinessLogic.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetSectionValue<T>(this IConfiguration configuration, string section) where T : new()
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            var setting = new T();
            configuration.GetSection(section).Bind(setting);
            return setting;
        }

        public static IConfigurationBuilder AddDbContextConfiguration(
            this IConfigurationBuilder configurationBuilder,
            Action<DbContextOptionsBuilder> builderAction)
        {
            configurationBuilder.Add(new DbContextConfigurationSource(builderAction));
            return configurationBuilder;
        }
    }
}