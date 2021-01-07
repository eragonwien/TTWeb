using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.AppSettings.Security;
using TTWeb.Data.Database;
using TTWeb.Data.Models;
using TTWeb.Web.Api.Extensions;
using TTWeb.Web.Api.Middlewares;
using TTWeb.Web.Api.Services;

namespace TTWeb.Web.Api
{
    public class Startup
    {
        private const string AllowSpecificOriginsPolicy = "AllowSpecificOrigins";
        public const string RequireManageDeploymentPermissionPolicy = "RequireManageDeploymentPermission";
        public const string RequireManageUsersPermissionPolicy = "RequireManageUsersPermission";
        public const string RequireAccessAllResourcesPermissionPolicy = "RequireAccessAllResourcesPermission";
        public const string RequireWorkerPermissionPolicy = "RequireIsWorkerPermission";
        public const string RequireManageWorkerPermissionPolicy = "RequireManageWorkerPermission";

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
                .RegisterEntityServices()
                .RegisterSwagger();

            services.AddScoped<IAccountService, AccountService>();

            var authenticationAppSettings =
                Configuration.GetSectionValue<AuthenticationAppSettings>(AuthenticationAppSettings.Section);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => ConfigureJwtBearerOptions(o, authenticationAppSettings));

            services.AddAuthorization(options =>
            {
                options.AddPolicy(RequireManageDeploymentPermissionPolicy,
                    p => p.RequireRole(UserPermission.ManageDeployment));
                options.AddPolicy(RequireManageUsersPermissionPolicy, p => p.RequireRole(UserPermission.ManageUsers));
                options.AddPolicy(RequireAccessAllResourcesPermissionPolicy,
                    p => p.RequireRole(UserPermission.AccessAllResources));
                options.AddPolicy(RequireWorkerPermissionPolicy,
                    p => p.RequireRole(UserPermission.IsWorker, UserPermission.ManageWorker));
                options.AddPolicy(RequireManageWorkerPermissionPolicy, p => p.RequireRole(UserPermission.ManageWorker));
            });

            var securityAppSettings = Configuration.GetSectionValue<SecurityAppSettings>(SecurityAppSettings.Section);
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOriginsPolicy,
                    b => { b.WithOrigins(securityAppSettings.Cors.Origins); });
            });

            services
                .AddControllers()
                .AddNewtonsoftJson(o => { o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseMiddleware<WebApiExceptionHandlerMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "TTWeb API V1"); });

            app.UseAuthentication();
            app.UseRouting();
            app.UseCors(AllowSpecificOriginsPolicy);
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireAuthorization(); });
        }

        private static void ConfigureJwtBearerOptions(JwtBearerOptions options,
            AuthenticationAppSettings authenticationAppSettings)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidIssuer = authenticationAppSettings.JsonWebToken.Issuer,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(authenticationAppSettings.JsonWebToken.AccessToken.Key))
            };
        }
    }
}