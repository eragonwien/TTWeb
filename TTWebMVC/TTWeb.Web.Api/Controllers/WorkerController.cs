using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Services.Worker;
using TTWeb.Web.Api.Services.Account;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/workers")]
    [ApiController] 
    [Authorize(Policy = Startup.RequireWorkerPermissionPolicy)]
    public class WorkerController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IWorkerService _workerService;

        public WorkerController(IAccountService accountService, 
            IWorkerService workerService)
        {
            _accountService = accountService;
            _workerService = workerService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<LoginTokenModel> WorkerLogin([FromBody] WorkerModel workerModel)
        {
            var authenticationResult = await _accountService.AuthenticateWorkerAsync(workerModel);

            if (!authenticationResult.Succeed) throw new UnauthorizedAccessException($"Authentication failed due to {authenticationResult.Reason}");
            return _accountService.GenerateAccessToken(authenticationResult.Result);
        }

        [HttpPost()]
        [Authorize(Policy = Startup.RequireManageWorkerPermissionPolicy)]
        public async Task<WorkerModel> Generate()
        {
            return await _workerService.GenerateAsync();
        }

        [HttpDelete]
        [Authorize(Policy = Startup.RequireManageWorkerPermissionPolicy)]
        public async Task Delete()
        {
            await _workerService.DeleteAsync();
        }
    }
}
