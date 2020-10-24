using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TTWeb.BusinessLogic.MappingProfiles;

namespace TTWeb.Web
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
    }
}
