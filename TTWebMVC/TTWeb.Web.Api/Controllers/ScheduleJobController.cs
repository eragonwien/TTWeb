using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Services.Schedule;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/schedule-jobs")]
    [ApiController]
    public class ScheduleJobController : BaseController
    {
        private readonly IScheduleJobService _scheduleJobService;

        public ScheduleJobController(IScheduleJobService scheduleJobService)
        {
            _scheduleJobService = scheduleJobService;
        }
    }
}
