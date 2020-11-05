using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.Data.Models;
using TTWeb.Web.Api.Components.Attributes;
using TTWeb.Web.Api.Middlewares;
using TTWeb.Web.Api.Extensions;

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

        private const string AllowSpecificOriginsPolicy = "AllowSpecificOrigins";
        public const string RequireManageDeploymentPermissionPolicy = "RequireManageDeploymentPermission";
        public const string RequireManageUsersPermissionPolicy = "RequireManageUsersPermission";
        public const string RequireAccessAllResourcesPermissionPolicy = "RequireAccessAllResourcesPermission";

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .RegisterDbContext(Configuration)
                .RegisterConfigurationOptions(Configuration)
                .RegisterAutoMapper()
                .RegisterEntityServices()
                .RegisterSwagger();

            var authenticationAppSettings = Configuration.GetSection(AuthenticationAppSettings.Section).Get<AuthenticationAppSettings>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => ConfigureJwtBearerOptions(o, authenticationAppSettings));

            services.AddAuthorization(options =>
            {
                options.AddPolicy(RequireManageDeploymentPermissionPolicy, p => p.RequireRole(UserPermission.ManageDeployment));
                options.AddPolicy(RequireManageUsersPermissionPolicy, p => p.RequireRole(UserPermission.ManageUsers));
                options.AddPolicy(RequireAccessAllResourcesPermissionPolicy, p => p.RequireRole(UserPermission.AccessAllResources));
            });

            var securityAppSettings = Configuration.GetSection(SecurityAppSettings.Section).Get<SecurityAppSettings>();
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOriginsPolicy, b =>
                {
                    b.WithOrigins(securityAppSettings.Cors.Origins);
                });
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new AccessOwnResourceFilterAttribute());
            });
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization(); 
            });
        }

        private static void ConfigureJwtBearerOptions(JwtBearerOptions options,
            AuthenticationAppSettings authenticationAppSettings)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(authenticationAppSettings.Methods.JsonWebToken.Secret)),
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}