using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TTWeb.BusinessLogic.Configurations;
using TTWeb.BusinessLogic.MappingProfiles;
using TTWeb.BusinessLogic.Services;

namespace TTWeb.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LoginUserProfile>();
            });

            services.AddSingleton(s => mapperConfig.CreateMapper());
            
            return services;
        }

        public static IServiceCollection RegisterAppSetting<T>(
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

            return services;
        }

        public static IServiceCollection RegisterEntityServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginUserService, LoginUserService>();
            services.AddScoped<ISeedService, SeedService>();

            return services;
        }
    }
}
