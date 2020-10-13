using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTWeb.Web.Extensions
{
    public static class ConfigurationExtension
    {
        public static void AddAppSetting<T>(
            this IServiceCollection services,
            IConfiguration configuration,
            string section)
            where T : class
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));
            if (section is null) throw new ArgumentNullException(nameof(section));


            var instance = Activator.CreateInstance<T>();
            configuration.Bind(section, instance);
            services.AddSingleton(instance);
        }
    }
}
