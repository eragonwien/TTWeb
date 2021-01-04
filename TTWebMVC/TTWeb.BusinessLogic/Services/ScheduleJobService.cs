using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public class ScheduleJobService : IScheduleJobService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;
        private readonly SchedulingJobAppSettings _jobAppSettings;

        public ScheduleJobService(TTWebContext context,
            IMapper mapper,
            IOptions<SchedulingAppSettings> schedulingAppSettingsOption)
        {
            _context = context;
            _mapper = mapper;
            _jobAppSettings = schedulingAppSettingsOption.Value.Job;
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

        private IQueryable<ScheduleJob> BaseQuery =>
            _context.ScheduleJobs
            .Include(j => j.Sender)
            .Include(j => j.Receiver);
    }
}