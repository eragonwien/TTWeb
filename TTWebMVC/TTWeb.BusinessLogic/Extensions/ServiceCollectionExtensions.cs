using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TTWeb.BusinessLogic.MappingProfiles;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Models.AppSettings.Security;
using TTWeb.BusinessLogic.Services;
using TTWeb.Data.Database;

namespace TTWeb.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); });

            services.AddSingleton(s => mapperConfig.CreateMapper());

            return services;
        }

        public static IServiceCollection RegisterEntityServices(this IServiceCollection services)
        {
            services.AddScoped<IHelperService, HelperService>();
            services.AddScoped<IEncryptionHelper, EncryptionHelper>();
            services.AddScoped<ILoginUserService, LoginUserService>();
            services.AddScoped<IAuthenticationHelperService, AuthenticationHelperService>();
            services.AddScoped<IFacebookUserService, FacebookUserService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IScheduleJobService, ScheduleJobService>();
            services.AddScoped<IScheduleJobResultService, ScheduleJobResultService>();
            return services;
        }

        public static IServiceCollection RegisterDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TTWebContext>(o => o.UseMySQL(configuration.GetConnectionString("TTWeb")));
            return services;
        }

        public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<AuthenticationAppSettings>(configuration.GetSection(AuthenticationAppSettings.Section));
            services.Configure<SecurityAppSettings>(configuration.GetSection(SecurityAppSettings.Section));
            services.Configure<SchedulingAppSettings>(configuration.GetSection(SchedulingAppSettings.Section));
            return services;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "TTWeb API", Version = "V1"}); });

            return services;
        }

        public static IServiceCollection ConfigureJsonOptions(this IServiceCollection services)
        {
            return services;
        }
    }
}