using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TTWeb.BusinessLogic.MappingProfiles;

namespace TTWeb.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LoginUserProfile>();
            });

            services.AddSingleton(s => mapperConfig.CreateMapper());
            
            return services;
        }

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
