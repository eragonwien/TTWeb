using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public class ScheduleJobService : IScheduleJobService
    {
        private readonly TTWebContext _context;
        private readonly SchedulingJobAppSettings _jobAppSettings;
        private readonly IMapper _mapper;

        public ScheduleJobService(TTWebContext context,
            IMapper mapper,
            IOptions<SchedulingAppSettings> schedulingAppSettingsOption)
        {
            _context = context;
            _mapper = mapper;
            _jobAppSettings = schedulingAppSettingsOption.Value.Job;
        }

        private IQueryable<ScheduleJob> BaseQuery =>
            _context.ScheduleJobs
                .Include(j => j.Sender)
                .Include(j => j.Receiver)
                .Include(j => j.Schedule);

        public async Task<ScheduleJobModel> GetOneAsync(int id)
        {
            var scheduleJob = await BaseQuery.SingleOrDefaultAsync(j => j.Id == id);

            return _mapper.Map<ScheduleJobModel>(scheduleJob);
        }

        public async Task<ICollection<ScheduleJobModel>> PeekAsync()
        {
            return await BaseQuery
                .AsNoTracking()
                .FilterOpenJobs()
                .Take(_jobAppSettings.CountPerRequest)
                .Select(j => _mapper.Map<ScheduleJobModel>(j))
                .ToListAsync();
        }

        public async Task ResetAsync(int id)
        {
            var scheduleJob = new ScheduleJob { Id = id };
            _context.ScheduleJobs.Attach(scheduleJob);


        }

        public async Task ResetAsync(ScheduleJobModel scheduleJobModel)
        {
            if (scheduleJobModel is null) return;

            var scheduleJob = _context.ScheduleJobs.GetOrAttachById(scheduleJobModel.Id);

            scheduleJob.Status = ProcessingStatus.New;
            scheduleJob.RetryCount = 0;
            scheduleJob.LockAt = null;
            scheduleJob.LockedUntil = null;
            scheduleJob.EndTime = null;

            await _context.SaveChangesAsync();
        }
    }
}