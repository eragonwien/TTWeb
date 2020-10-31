using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TTWeb.BusinessLogic.Configurations;
using TTWeb.BusinessLogic.MappingProfiles;
using TTWeb.BusinessLogic.Services;
using TTWeb.Data.Database;

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

            return services;
        }

        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TTWebContext>(o => o.UseSqlServer(configuration.GetConnectionString("TTWeb")));
            return services;
        }

        public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProvidersAppSettings>(configuration.GetSection("Providers"));
            return services;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TTWeb API", Version = "V1" });
            });

            return services;
        }
    }
}
