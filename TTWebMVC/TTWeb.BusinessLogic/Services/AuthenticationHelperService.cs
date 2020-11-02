using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Models.Account;

namespace TTWeb.BusinessLogic.Services
{
    public class AuthenticationHelperService : IAuthenticationHelperService
    {
        private readonly IHostEnvironment _env;

        public AuthenticationHelperService(IHostEnvironment env)
        {
            _env = env;
        }

#pragma warning disable CS1998 // TODO: Adds facebook validation
        public async Task<bool> IsTokenValidAsync(ExternalLoginModel loginModel)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (loginModel is null) throw new System.ArgumentNullException(nameof(loginModel));

            if (_env.IsDevelopment()) return true;
            return false;
        }
    }
}