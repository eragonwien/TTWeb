using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using SNGCommon;
using TTWebApi.Services;
using TTWebCommon.Models;
using TTWebMVCV2.Models;

namespace TTWebMVCV2
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
         // AppSettings 
         var appSettingsSection = Configuration.GetSection("AppSettings");
         services.Configure<AppSettings>(appSettingsSection);

         // registers services und dbcontext
         services.AddTransient(_ => new TTWebDbContext(Configuration.GetConnectionString("TTWeb")));
         services.AddScoped<IAppUserService, AppUserService>();
         
         // authentication
         services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(co =>
            {
               co.LoginPath = "/Account/Login";
            })
            .AddCookie(AuthenticationSettings.SchemeExternal)
            .AddFacebook(o =>
            {
               o.SignInScheme = AuthenticationSettings.SchemeExternal;
               o.AppId = Configuration["Authentication:Facebook:AppId"];
               o.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

         // authorization
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
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error/Index");
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
   }
}
