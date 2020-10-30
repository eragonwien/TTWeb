using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.Web.Api.Middlewares;

namespace TTWeb.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services
                .RegisterDbContext(Configuration)
                .RegisterConfigurationOptions(Configuration)
                .RegisterAutoMapper()
                .RegisterEntityServices();

            services.AddAuthorization();

            services.AddControllers(o =>
            {
                if (Environment.IsDevelopment())
                {
                    o.Filters.Add<AllowAnonymousFilter>();
                }
                else
                {
                    o.Filters.Add(new AuthorizeFilter(DefaultAuthorizationPolicy));
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthorization();
            app.UseMiddleware<WebApiExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private AuthorizationPolicy DefaultAuthorizationPolicy => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    }
}
