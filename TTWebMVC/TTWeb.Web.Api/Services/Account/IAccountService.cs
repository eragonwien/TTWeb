﻿using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;

namespace TTWeb.Web.Api.Services.Account
{
    public interface IAccountService
    {
        Task<ProcessingResult<LoginUserModel>> AuthenticateExternalAsync(ExternalLoginModel loginModel);
        Task<ProcessingResult<WorkerModel>> AuthenticateWorkerAsync(WorkerModel model);
        LoginTokenModel GenerateAccessToken(LoginUserModel user);
        LoginTokenModel GenerateAccessToken(WorkerModel worker);
        Task<LoginTokenModel> RefreshAccessToken(LoginTokenModel loginTokenModel);
    }
}