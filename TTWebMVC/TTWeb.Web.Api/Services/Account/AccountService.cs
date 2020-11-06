using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.BusinessLogic.Services.Authentication;
using TTWeb.BusinessLogic.Services.LoginUser;

namespace TTWeb.Web.Api.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly AuthenticationAppSettings _authenticationAppSettings;
        private readonly IAuthenticationHelperService _authHelperService;
        private readonly ILoginUserService _loginUserService;
        private readonly IMapper _mapper;

        public AccountService(IOptions<AuthenticationAppSettings> authenticationAppSettings,
            IAuthenticationHelperService authHelperService,
            ILoginUserService loginUserService,
            IMapper mapper)
        {
            _authenticationAppSettings = authenticationAppSettings.Value;
            _authHelperService = authHelperService;
            _loginUserService = loginUserService;
            _mapper = mapper;
        }

        public async Task<ProcessingResult<LoginUserModel>> AuthenticateExternalAsync(ExternalLoginModel loginModel)
        {
            var result = new ProcessingResult<LoginUserModel>();
            if (!await _authHelperService.IsTokenValidAsync(loginModel))
                return result.WithSuccess(false).WithReason("token is invalid");

            var loginUserModel = _mapper.Map<LoginUserModel>(loginModel);
            loginUserModel = await _loginUserService.GetByEmailAsync(loginUserModel.Email);
            loginUserModel ??= await _loginUserService.CreateAsync(loginUserModel);

            return result.WithSuccess().WithResult(loginUserModel);
        }

        public LoginTokenModel GenerateLoginToken(LoginUserModel user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var tokenHandler = new JwtSecurityTokenHandler();
            var loginTokenModel = new LoginTokenModel();

            var accessToken = CreateSecurityToken(
                _authenticationAppSettings.Methods.JsonWebToken.Secret,
                DateTime.UtcNow.AddMinutes(_authenticationAppSettings.Methods.JsonWebToken.AccessTokenDurationMinutes),
                tokenHandler, new ClaimsIdentity(GetUserClaims(user)));
            loginTokenModel.AccessToken.Token = tokenHandler.WriteToken(accessToken);
            loginTokenModel.AccessToken.ExpirationDateUtc = accessToken.ValidTo;

            var refreshToken = CreateSecurityToken(
                _authenticationAppSettings.Methods.JsonWebToken.Secret,
                DateTime.UtcNow.AddMinutes(_authenticationAppSettings.Methods.JsonWebToken.RefreshTokenDurationDays),
                tokenHandler);
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

            return tokenHandler.CreateJwtSecurityToken(issuer: _authenticationAppSettings.Methods.JsonWebToken.Issuer,
                subject: subject, expires: expirationDateUtc, signingCredentials: signingCredentials);
        }

        private static IEnumerable<Claim> GetUserClaims(LoginUserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };
            claims.AddRange(user.UserPermissions.Select(p => new Claim(ClaimTypes.Role, p.ToString())));

            return claims;
        }
    }
}
