using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.Authentication
{
    public interface IAuthenticationHelperService
    {
        Task<bool> IsExternalAccessTokenValidAsync(ExternalLoginModel loginModel);

        bool IsAlmostExpired(DateTime expirationDate, TimeSpan maxDuration);

        IEnumerable<Claim> GenerateClaims(LoginUserModel loginUserModel);

        IEnumerable<Claim> GenerateClaims(WorkerModel workerModel);
    }
}