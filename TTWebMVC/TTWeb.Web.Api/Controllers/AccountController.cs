using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Services;

namespace TTWeb.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IExternalAuthenticationService _externalAuthService;
        private readonly ILoginUserService _loginUserService;
        private readonly ILogger<AccountController> _log;

        public AccountController(
            IExternalAuthenticationService externalAuthService,
            ILoginUserService loginUserService,
            ILogger<AccountController> log)
        {
            _externalAuthService = externalAuthService;
            _loginUserService = loginUserService;
            _log = log;
        }


        // POST api/<AccountController>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var loginUserModel = await _externalAuthService.ValidateTokenAsync(loginModel);

            // creates new exception and handles it in global handler
            if (loginUserModel == null) throw new Exception(nameof(loginUserModel));

            loginUserModel = await _loginUserService.GetOrAddUserAsync(loginUserModel);

            // should never happens -> 500
            if (loginUserModel == null) throw new Exception(nameof(loginUserModel));

            var tokenModel = await GetNewToken();

            return Ok(tokenModel);
        }

        private async Task<object> GetNewToken()
        {
            throw new NotImplementedException();
        }
    }
}
