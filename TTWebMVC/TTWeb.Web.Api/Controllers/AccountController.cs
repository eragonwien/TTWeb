using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Web.Api.Services.Account;

namespace TTWeb.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<LoginTokenModel> Login([FromBody] ExternalLoginModel loginModel)
        {
            var authenticationResult = await _accountService.AuthenticateExternalAsync(loginModel);

            if (!authenticationResult.Succeed) throw new UnauthorizedAccessException($"Authentication failed due to {authenticationResult.Reason}");
            return _accountService.GenerateAccessToken(authenticationResult.Result);
        }

        [HttpPost("box-login")]
        [AllowAnonymous]
        public async Task<LoginTokenModel> BoxLogin([FromBody] WorkerModel loginModel)
        {
            var authenticationResult = await _accountService.AuthenticateBoxAsync(loginModel);

            if (!authenticationResult.Succeed) throw new UnauthorizedAccessException($"Authentication failed due to {authenticationResult.Reason}");
            return _accountService.GenerateAccessToken(authenticationResult.Result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<LoginTokenModel> RefreshToken([FromBody] LoginTokenModel loginTokenModel)
        {
            return await _accountService.RefreshAccessToken(loginTokenModel);
        }
    }
}