using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        private readonly IAuthenticationHelperService _authHelperService;
        private readonly ILoginUserService _loginUserService;
        private readonly IMapper _mapper;
        private readonly AuthenticationAppSettings _authenticationAppSettings;

        public AccountController(IAuthenticationHelperService authHelperService,
            ILoginUserService loginUserService,
            IMapper mapper,
            IOptions<AuthenticationAppSettings> authenticationAppSettings)
        {
            _authHelperService = authHelperService;
            _loginUserService = loginUserService;
            _mapper = mapper;
            _authenticationAppSettings = authenticationAppSettings.Value;
        }

        // POST api/<AccountController>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] ExternalLoginModel loginModel)
        {
            if (!ModelState.IsValid) throw new InvalidInputException(ModelState); 
            if (!await _authHelperService.IsTokenValidAsync(loginModel)) throw new InvalidTokenException();

            var loginUserModel = _mapper.Map<LoginUserModel>(loginModel);
            loginUserModel = await _loginUserService.GetUserByEmailAsync(loginUserModel.Email);
            loginUserModel ??= await _loginUserService.CreateUserAsync(loginUserModel);
            
            var loginTokenModel = CreateLoginTokenModel(loginUserModel);
            return Ok(loginTokenModel);
        }

        private LoginTokenModel CreateLoginTokenModel(LoginUserModel user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var tokenHandler = new JwtSecurityTokenHandler();
            var loginTokenModel = new LoginTokenModel();

            var accessToken = CreateSecurityToken(
                _authenticationAppSettings.Methods.JsonWebToken.Secret,
                DateTime.UtcNow.AddMinutes(_authenticationAppSettings.Methods.JsonWebToken.AccessTokenDurationMinutes), tokenHandler, new ClaimsIdentity(GetUserClaims(user)));
            loginTokenModel.AccessToken.Token = tokenHandler.WriteToken(accessToken);
            loginTokenModel.AccessToken.ExpirationDateUtc = accessToken.ValidTo;

            var refreshToken = CreateSecurityToken(
                _authenticationAppSettings.Methods.JsonWebToken.Secret,
                DateTime.UtcNow.AddMinutes(_authenticationAppSettings.Methods.JsonWebToken.RefreshTokenDurationDays), tokenHandler);
            loginTokenModel.RefreshToken.Token = tokenHandler.WriteToken(refreshToken);
            loginTokenModel.RefreshToken.ExpirationDateUtc = refreshToken.ValidTo;

            return loginTokenModel;
        }

        private JwtSecurityToken CreateSecurityToken(string secretKey,
            DateTime expirationDateUtc,
            JwtSecurityTokenHandler tokenHandler,
            ClaimsIdentity subject = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            return tokenHandler.CreateJwtSecurityToken(issuer: _authenticationAppSettings.Methods.JsonWebToken.Issuer, subject: subject, expires: expirationDateUtc, signingCredentials: signingCredentials);
        }

        private IEnumerable<Claim> GetUserClaims(LoginUserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };
            claims.AddRange(user.UserPermissions.Select(p => new Claim(ClaimTypes.Role, p.ToString())));

            return claims;
        }
    }
}
