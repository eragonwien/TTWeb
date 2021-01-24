﻿using System;
using System.Collections.Generic;
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
using TTWeb.BusinessLogic.Services;
using TTWeb.Web.Api.Extensions;

namespace TTWeb.Web.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthenticationHelperService _authHelperService;
        private readonly AuthenticationAppSettings _authSettings;
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

            var userClaims = _authHelperService.GenerateClaims(user);

            return BuildLoginTokenModel(userClaims, _authSettings.JsonWebToken);
        }

        public async Task<LoginTokenModel> RefreshAccessToken(LoginTokenModel loginTokenModel)
        {
            var accessTokenValidation = _tokenHandler.ValidateToken(loginTokenModel.AccessToken,
                _authSettings.JsonWebToken.TokenValidationDefaultParameters
                    .WithKey(_authSettings.JsonWebToken.AccessToken.Key).ValidateLifeTime(false));

            if (!accessTokenValidation.Succeed) throw new UnauthorizedAccessException();

            var refreshTokenValidation = _tokenHandler.ValidateToken(loginTokenModel.RefreshToken,
                _authSettings.JsonWebToken.TokenValidationDefaultParameters.WithKey(_authSettings.JsonWebToken
                    .RefreshToken.Key));

            if (!refreshTokenValidation.Succeed) throw new UnauthorizedAccessException();

            var loginUser =
                await _loginUserService.GetByIdAsync(
                    accessTokenValidation.TokenUser.FindFirstValue<int>(ClaimTypes.NameIdentifier));
            if (loginUser == null) throw new UnauthorizedAccessException();

            var accessToken =
                _tokenHandler.CreateAccessToken(_authSettings.JsonWebToken, accessTokenValidation.TokenUser.Claims);
            loginTokenModel.AccessToken.Token = _tokenHandler.WriteToken(accessToken);
            loginTokenModel.AccessToken.ExpirationDateUtc = accessToken.ValidTo;

            if (_authHelperService.IsAlmostExpired(refreshTokenValidation.Token.ValidTo,
                _authSettings.JsonWebToken.RefreshToken.Duration))
            {
                var refreshToken = _tokenHandler.CreateRefreshToken(_authSettings.JsonWebToken);
                loginTokenModel.RefreshToken.Token = _tokenHandler.WriteToken(refreshToken);
                loginTokenModel.RefreshToken.ExpirationDateUtc = refreshToken.ValidTo;
            }

            return loginTokenModel;
        }

        private LoginTokenModel BuildLoginTokenModel(IEnumerable<Claim> userClaims,
            AuthenticationJsonWebTokenAppSettings jwtSettings)
        {
            if (userClaims is null) throw new ArgumentNullException(nameof(userClaims));

            var loginTokenModel = new LoginTokenModel();

            var accessToken = _tokenHandler.CreateAccessToken(jwtSettings, userClaims);
            loginTokenModel.AccessToken.Token = _tokenHandler.WriteToken(accessToken);
            loginTokenModel.AccessToken.ExpirationDateUtc = accessToken.ValidTo;

            var refreshToken = _tokenHandler.CreateRefreshToken(jwtSettings);
            loginTokenModel.RefreshToken.Token = _tokenHandler.WriteToken(refreshToken);
            loginTokenModel.RefreshToken.ExpirationDateUtc = refreshToken.ValidTo;

            return loginTokenModel;
        }
    }
}