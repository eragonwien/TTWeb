using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Services;
using TTWeb.Web.Api.Services.Account;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/workers")]
    [ApiController] 
    [Authorize(Policy = Startup.RequireManageWorkerPermissionPolicy)]
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

        [HttpPost]
        public async Task<WorkerModel> Generate()
        {
            return await _workerService.GenerateAsync();
        }

        [HttpGet]
        public async Task<List<WorkerModel>> Read()
        {
            return await _workerService.ReadAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _workerService.DeleteAsync(id);
        }
    }
}
