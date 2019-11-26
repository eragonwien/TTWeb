using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SNGCommon.Common;
using System.Collections.Generic;
using System.Globalization;

namespace TTWebMVC
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
         services.Configure<CookiePolicyOptions>(options =>
         {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.Lax;
         });

         var cultureDe = new CultureInfo(Settings.CultureGerman);
         cultureDe.NumberFormat.CurrencyDecimalSeparator = ".";
         cultureDe.NumberFormat.NumberDecimalSeparator = ".";
         var cultureEn = new CultureInfo(Settings.CultureEnglish);
         cultureEn.NumberFormat.CurrencyDecimalSeparator = ".";
         cultureEn.NumberFormat.NumberDecimalSeparator = ".";

         var supportedCultures = new List<CultureInfo> { cultureDe, cultureEn };
         services.Configure<RequestLocalizationOptions>(o =>
         {
            o.DefaultRequestCulture = new RequestCulture(cultureDe);
            o.SupportedCultures = supportedCultures;
            o.SupportedUICultures = supportedCultures;
         });

         services
            .AddAuthentication(AuthenticationSettings.SchemeApplication)
            .AddCookie(AuthenticationSettings.SchemeApplication)
            .AddCookie(AuthenticationSettings.SchemeExternal)
            .AddFacebook(o =>
            {
               o.SignInScheme = AuthenticationSettings.SchemeExternal;
               o.AppId = Configuration["Facebook:AppId"];
               o.AppSecret = Configuration["Facebook:AppSecret"];
               o.SaveTokens = true;
            });

         services.Configure<CookieAuthenticationOptions>(o =>
         {
            o.LoginPath = "/Account/Login";
            o.LogoutPath = "/Account/Logout";
            o.AccessDeniedPath = "/Error/403";
            o.SlidingExpiration = true;
         });

         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseRequestLocalization();
         app.UseHttpsRedirection();
         app.UseStaticFiles();
         app.UseCookiePolicy();
         app.UseAuthentication();

         app.UseMvc(routes =>
         {
            routes.MapRoute(
               name: "default",
               template: "{controller=Account}/{action=Login}/{id?}",
               defaults: new { controller = "Home", action = "Index" },
               constraints: new { controller = "Home|Account|Error" }
            );

            routes.MapRoute(
               name: "notfound",
               template: "{*url}",
               defaults: new { controller = "Error", action = "PageNotFound" }
            );
         });
      }
   }
}
