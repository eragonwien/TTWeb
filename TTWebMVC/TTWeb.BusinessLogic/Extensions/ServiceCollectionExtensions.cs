﻿using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TTWeb.BusinessLogic.MappingProfiles;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Services.Authentication;
using TTWeb.BusinessLogic.Services.Encryption;
using TTWeb.BusinessLogic.Services.Facebook;
using TTWeb.BusinessLogic.Services.LoginUser;
using TTWeb.Data.Database;

namespace TTWeb.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LoginUserProfile>();
                c.AddProfile<FacebookUserProfile>();
            });

            services.AddSingleton(s => mapperConfig.CreateMapper());

            return services;
        }

        public static IServiceCollection RegisterEntityServices(this IServiceCollection services)
        {
            services.AddScoped<IEncryptionHelper, EncryptionHelper>();
            services.AddScoped<ILoginUserService, LoginUserService>();
            services.AddScoped<IAuthenticationHelperService, AuthenticationHelperService>();
            services.AddScoped<IFacebookUserService, FacebookUserService>();
            return services;
        }

        public static IServiceCollection RegisterDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TTWebContext>(o => o.UseSqlServer(configuration.GetConnectionString("TTWeb")));
            return services;
        }

        public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<AuthenticationAppSettings>(configuration.GetSection(AuthenticationAppSettings.Section));
            services.Configure<EncryptionAppSettings>(configuration.GetSection(EncryptionAppSettings.Section));
            return services;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "TTWeb API", Version = "V1"}); });

            return services;
        }
    }
}