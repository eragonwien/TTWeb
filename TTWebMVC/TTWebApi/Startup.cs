using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SNGCommon.Authentication;
using System;
using System.Text;
using TTWebApi.Middlewares;
using TTWebApi.Models;
using TTWebApi.Services;
using TTWebCommon.Models;

namespace TTWebApi
{
   public class Startup
   {
      public Startup(IConfiguration configuration, IHostingEnvironment env)
      {
         Configuration = configuration;
         Environment = env;
      }

      public IConfiguration Configuration { get; }
      public IHostingEnvironment Environment { get; set; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddCors();
         services.AddDbContext<TTWebDbContext>(o => o.UseMySQL(Configuration.GetConnectionString("TTWeb")));

         var appSettingsSection = Configuration.GetSection("AppSettings");
         services.Configure<AppSettings>(appSettingsSection);

         services.AddScoped<IAppUserService, AppUserService>();
         services.AddScoped<IAccountService, AccountService>();
         services.AddScoped<IAuthenticationService, AuthenticationService>();
         services.AddScoped<IScheduleJobService, ScheduleJobService>();

         var appsettings = appSettingsSection.Get<AppSettings>();

         services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
               o.RequireHttpsMetadata = false;
               o.SaveToken = true;
               o.TokenValidationParameters = new TokenValidationParameters
               {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appsettings.AuthSecret)),
                  ValidateIssuer = false,
                  ValidateAudience = false,
                  ClockSkew = TimeSpan.Zero
               };
            });

         services
            .AddMvc(o =>
            {
               if (!Environment.IsDevelopment())
               {
                  o.Filters.Add(new AuthorizeFilter());
               }
            })
            .AddJsonOptions(o =>
            {
               o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
               o.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
               o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseCors(o =>
         {
            o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
         });
         app.UseAuthentication();

         app.UseHttpsRedirection();
         app.UseMiddleware(typeof(ErrorHandlingMiddleware));
         app.UseMvc();
      }
   }
}
