using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.AppSettings;
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
        private readonly AuthenticationAppSettings _authenticationAppSettings;

        public AccountController(
            IExternalAuthenticationService externalAuthService,
            ILoginUserService loginUserService,
            IMapper mapper,
            IOptions<AuthenticationAppSettings> authenticationAppSettings)
        {
            _externalAuthService = externalAuthService;
            _loginUserService = loginUserService;
            _mapper = mapper;
            _authenticationAppSettings = authenticationAppSettings.Value;
        }


        // POST api/<AccountController>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] ExternalLoginModel loginModel)
        {
            if (!await _externalAuthService.IsTokenValidAsync(loginModel)) throw new InvalidTokenException();

            LoginUserModel loginUserModel = _mapper.Map<LoginUserModel>(loginModel);
            loginUserModel = await _loginUserService.GetUserByEmailAsync(loginUserModel.Email);

            loginUserModel ??= await _loginUserService.CreateUserAsync(loginUserModel);

            loginUserModel.Token = GenerateToken();

            return Ok(loginUserModel);
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationAppSettings.Methods.JWT.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _authenticationAppSettings.Methods.JWT.Issuer,
                null,
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
