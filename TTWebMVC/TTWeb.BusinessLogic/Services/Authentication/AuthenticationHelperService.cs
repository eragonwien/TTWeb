using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities;

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
            var timeLeft = expirationDate - DateTime.UtcNow;
            return timeLeft <= tolerantTimeSpan;
        }

        public IEnumerable<Claim> GenerateClaims(LoginUserModel loginUserModel)
        {
            yield return new Claim(ClaimTypes.NameIdentifier, loginUserModel.Id.ToString());
            yield return new Claim(ClaimTypes.Email, loginUserModel.Email);
            yield return new Claim(ClaimTypes.GivenName, loginUserModel.FirstName);
            yield return new Claim(ClaimTypes.Surname, loginUserModel.LastName);

            foreach (var permission in loginUserModel.Permissions)
                yield return new Claim(ClaimTypes.Role, permission.ToString());
        }

        public IEnumerable<Claim> GenerateClaims(WorkerModel workerModel)
        {
            yield return new Claim(ClaimTypes.NameIdentifier, workerModel.Id.ToString());

            foreach (var permission in workerModel.Permissions)
                yield return new Claim(ClaimTypes.Role, permission.ToString());
        }
    }
}