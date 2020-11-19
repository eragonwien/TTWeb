using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.BusinessLogic.Services.Authentication;
using TTWeb.BusinessLogic.Services.LoginUser;
using TTWeb.Web.Api.Extensions;

namespace TTWeb.Web.Api.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly AuthenticationAppSettings _authSettings;
        private readonly IAuthenticationHelperService _authHelperService;
        private readonly ILoginUserService _loginUserService;
        private readonly IMapper _mapper;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AccountService(IOptions<AuthenticationAppSettings> authenticationAppSettings,
            IAuthenticationHelperService authHelperService,
            ILoginUserService loginUserService,
            IMapper mapper)
        {
            _authSettings = authenticationAppSettings.Value;
            _authHelperService = authHelperService;
            _loginUserService = loginUserService;
            _mapper = mapper;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public Task<ProcessingResult<WorkerModel>> AuthenticateBoxAsync(WorkerModel loginModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ProcessingResult<LoginUserModel>> AuthenticateExternalAsync(ExternalLoginModel loginModel)
        {
            var result = new ProcessingResult<LoginUserModel>();
            if (!await _authHelperService.IsExternalAccessTokenValidAsync(loginModel))
                return result.WithSuccess(false).WithReason("token is invalid");

            var loginUserModel = _mapper.Map<LoginUserModel>(loginModel);
            loginUserModel = await _loginUserService.GetByEmailAsync(loginUserModel.Email);
            loginUserModel ??= await _loginUserService.CreateAsync(loginUserModel);

            return result.WithSuccess().WithResult(loginUserModel);
        }

        public LoginTokenModel GenerateAccessToken(LoginUserModel user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var loginTokenModel = new LoginTokenModel();

            var userClaims = _authHelperService.GenerateClaims(user);
            var accessToken = _tokenHandler.CreateAccessToken(_authSettings.JsonWebToken, userClaims);
            loginTokenModel.AccessToken.Token = _tokenHandler.WriteToken(accessToken);
            loginTokenModel.AccessToken.ExpirationDateUtc = accessToken.ValidTo;

            var refreshToken = _tokenHandler.CreateRefreshToken(_authSettings.JsonWebToken);
            loginTokenModel.RefreshToken.Token = _tokenHandler.WriteToken(refreshToken);
            loginTokenModel.RefreshToken.ExpirationDateUtc = refreshToken.ValidTo;

            return loginTokenModel;
        }

        public LoginTokenModel GenerateAccessToken(WorkerModel worker)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginTokenModel> RefreshAccessToken(LoginTokenModel loginTokenModel)
        {
            var accessTokenValidation = _tokenHandler.ValidateToken(loginTokenModel.AccessToken,
                _authSettings.JsonWebToken.TokenValidationDefaultParameters.WithKey(_authSettings.JsonWebToken.AccessToken.Key).ValidateLifeTime(false));

            if (!accessTokenValidation.Succeed) throw new UnauthorizedAccessException();

            var refreshTokenValidation = _tokenHandler.ValidateToken(loginTokenModel.RefreshToken,
                _authSettings.JsonWebToken.TokenValidationDefaultParameters.WithKey(_authSettings.JsonWebToken.RefreshToken.Key));

            if (!refreshTokenValidation.Succeed) throw new UnauthorizedAccessException();

            var loginUser = await _loginUserService.GetByIdAsync(accessTokenValidation.TokenUser.FindFirstValue<int>(ClaimTypes.NameIdentifier));
            if (loginUser == null) throw new UnauthorizedAccessException();

            var accessToken = _tokenHandler.CreateAccessToken(_authSettings.JsonWebToken, accessTokenValidation.TokenUser.Claims);
            loginTokenModel.AccessToken.Token = _tokenHandler.WriteToken(accessToken);
            loginTokenModel.AccessToken.ExpirationDateUtc = accessToken.ValidTo;

            if (_authHelperService.IsAlmostExpired(refreshTokenValidation.Token.ValidTo, _authSettings.JsonWebToken.RefreshToken.Duration))
            {
                var refreshToken = _tokenHandler.CreateRefreshToken(_authSettings.JsonWebToken);
                loginTokenModel.RefreshToken.Token = _tokenHandler.WriteToken(refreshToken);
                loginTokenModel.RefreshToken.ExpirationDateUtc = refreshToken.ValidTo;
            }

            return loginTokenModel;
        }
    }
}
