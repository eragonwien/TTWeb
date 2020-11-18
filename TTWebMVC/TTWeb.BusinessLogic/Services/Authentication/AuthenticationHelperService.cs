﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Models.Account;

namespace TTWeb.BusinessLogic.Services.Authentication
{
    public class AuthenticationHelperService : IAuthenticationHelperService
    {
        private readonly IHostEnvironment _env;

        public AuthenticationHelperService(IHostEnvironment env)
        {
            _env = env;
        }

#pragma warning disable CS1998 // TODO: Adds external provider access token validation
        public async Task<bool> IsExternalAccessTokenValidAsync(ExternalLoginModel loginModel)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (loginModel is null) throw new System.ArgumentNullException(nameof(loginModel));

            if (_env.IsDevelopment()) return true;
            return false;
        }

        public bool IsAlmostExpired(DateTime expirationDate, TimeSpan maxDuration)
        {
            var tolerantTimeSpan = new TimeSpan(maxDuration.Ticks / 2);
            var timeLeft = DateTime.UtcNow - expirationDate;
            return timeLeft <= tolerantTimeSpan;
        }
    }
}