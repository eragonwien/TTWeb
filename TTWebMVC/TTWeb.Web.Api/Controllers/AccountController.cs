using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities.LoginUser;
using TTWeb.BusinessLogic.Services;

namespace TTWeb.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IExternalAuthenticationService _externalAuthService;
        private readonly ILoginUserService _loginUserService;
        private readonly IMapper _mapper;

        public AccountController(
            IExternalAuthenticationService externalAuthService,
            ILoginUserService loginUserService,
            IMapper mapper)
        {
            _externalAuthService = externalAuthService;
            _loginUserService = loginUserService;
            _mapper = mapper;
        }


        // POST api/<AccountController>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] ExternalLoginModel loginModel)
        {
            if (!await _externalAuthService.IsTokenValidAsync(loginModel)) throw new InvalidTokenException();

            var loginUserModel = _mapper.Map<LoginUserModel>(loginModel);
            loginUserModel = await _loginUserService.GetOrAddUserAsync(loginUserModel);

            if (loginUserModel == null) throw new InsertOperationFailedException(nameof(loginUserModel));

            var tokenModel = await GetNewToken();

            return Ok(tokenModel);
        }

        private async Task<object> GetNewToken()
        {
            throw new NotImplementedException();
        }
    }
}
