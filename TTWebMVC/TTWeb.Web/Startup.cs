using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Configurations;
using TTWeb.BusinessLogic.Services;
using TTWeb.Data.Database;
using TTWeb.Web.Extensions;

namespace TTWeb.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<TTWebContext>(o => o.UseMySql(Configuration.GetConnectionString("Default")));
            services.AddAppSetting<AuthenticationAppSetting>(Configuration, AuthenticationAppSetting.SectionName);

            services.AddScoped<ISeedService, SeedService>();

            services.ConfigureApplicationCookie(o =>
            {
                o.Cookie.SameSite = SameSiteMode.Strict;
            });

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddFacebook(o =>
                {
                    o.SignInScheme = Configuration["Authentication:ExternalCookieScheme"];
                    o.AppId = Configuration["Authentication:Facebook:AppId"];
                    o.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                });

            services.AddAuthorization();
            var authenticatedPolicy = new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .Build();
            services.AddControllersWithViews(o =>
            {
                o.Filters.Add(new AuthorizeFilter(authenticatedPolicy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app, env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Contents")),
                RequestPath = new PathString("/Contents")
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var seedService = scope.ServiceProvider.GetRequiredService<ISeedService>();
            seedService.Migrate();

            if (env.IsDevelopment())
                seedService.Seed();
        }
    }
}
