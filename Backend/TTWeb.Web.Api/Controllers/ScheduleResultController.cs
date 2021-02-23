using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models;
using TTWeb.BusinessLogic.Services;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/schedule-results")]
    [ApiController]
    public class ScheduleResultController : BaseController
    {
        private readonly IScheduleJobResultService _service;

        public ScheduleResultController(IScheduleJobResultService service)
        {
            _service = service;
        }

        [HttpGet("")]
        [Authorize(Policy = Startup.RequireAccessAllResourcesPermissionPolicy)]
        public async Task<IEnumerable<ScheduleJobResultModel>> ReadMany([FromQuery] int scheduleJobId)
        {
            return await _service.ReadManyAsync(scheduleJobId);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Startup.RequireAccessAllResourcesPermissionPolicy)]
        public async Task<ScheduleJobResultModel> ReadOne([FromRoute] int id)
        {
            var result = await _service.ReadOneAsync(id);
            //TODO: ThrowExceptionOnWrongOwner(result.OwnerId);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Startup.RequireAccessAllResourcesPermissionPolicy)]
        public async Task DeleteOne([FromRoute] int id)
        {
            await _service.DeleteOneAsync(id, OwnerId);
        }
    }
}