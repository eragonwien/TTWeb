using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TTWeb.Web.Api.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            var serviceScopeFactory = (IServiceScopeFactory) host.Services.GetService(typeof(IServiceScopeFactory));

            using var scope = serviceScopeFactory.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<T>();

            dbContext.Database.Migrate();

            return host;
        }
    }
}