using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.AppSettings;
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

        private AuthorizationPolicy DefaultAuthorizationPolicy =>
            new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .RegisterDbContext(Configuration)
                .RegisterConfigurationOptions(Configuration)
                .RegisterAutoMapper()
                .RegisterEntityServices()
                .RegisterSwagger();

            var authenticationAppSettings = Configuration.GetSection("Authentication").Get<AuthenticationAppSettings>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => ConfigureJwtBearerOptions(o, authenticationAppSettings));

            services.AddAuthorization();

            services.AddControllers(o =>
            {
                if (Environment.IsDevelopment())
                    o.Filters.Add<AllowAnonymousFilter>();
                else
                    o.Filters.Add(new AuthorizeFilter(DefaultAuthorizationPolicy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseMiddleware<WebApiExceptionHandlerMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "TTWeb API V1"); });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void ConfigureJwtBearerOptions(JwtBearerOptions options,
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