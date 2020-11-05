using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : BaseController
    {
        [HttpPost]
        public async Task<ScheduleModel> Create([FromBody] ScheduleModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<ScheduleModel>> Read()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ScheduleModel> ReadOne([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{id}")]
        public async Task<ScheduleModel> Update([FromRoute] int id, [FromBody] ScheduleModel model)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<ScheduleModel> Delete([FromRoute] int id)
        {
            throw new NotImplementedException();
        }
    }
}
