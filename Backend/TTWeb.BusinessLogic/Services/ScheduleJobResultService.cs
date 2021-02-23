using System;
using System.Threading.Tasks;
using AutoMapper;
using TTWeb.BusinessLogic.Models;
using TTWeb.Data.Database;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public class ScheduleJobResultService : IScheduleJobResultService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;

        public ScheduleJobResultService(TTWebContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(ScheduleJobResultModel scheduleJobResultModel)
        {
            if (scheduleJobResultModel == null) throw new ArgumentNullException(nameof(scheduleJobResultModel));

            var jobResult = _mapper.Map<ScheduleJobResult>(scheduleJobResultModel);
            await _context.ScheduleJobsResults.AddAsync(jobResult);
            await _context.SaveChangesAsync();
        }
    }
}