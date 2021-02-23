using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface IAuthenticationHelperService
    {
        Task<bool> IsExternalAccessTokenValidAsync(ExternalLoginModel loginModel);

        bool IsAlmostExpired(DateTime expirationDate, TimeSpan maxDuration);

        IEnumerable<Claim> GenerateClaims(LoginUserModel loginUserModel);
    }
}