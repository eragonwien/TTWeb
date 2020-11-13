﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.Data.Database;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public class ScheduleJobService : IScheduleJobService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;

        public ScheduleJobService(TTWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ProcessingResult<ScheduleJobModel>> PlanJob(IEnumerable<Data.Models.Schedule> schedules)
        {
            if (schedules == null) throw new ArgumentNullException(nameof(schedules));

            var results = new List<ProcessingResult<ScheduleJobModel>>();

            foreach (var schedule in schedules)
            {
                var result = new ProcessingResult<ScheduleJobModel>();
                try
                {
                    var receiverIds = schedule.ScheduleReceiverMappings.Select(m => m.ReceiverId).Distinct().ToList();
                    var receiverTimeFrames = receiverIds.Zip(schedule.TimeFrames, Tuple.Create);
                    foreach (var (receiverId, timeFrame) in receiverTimeFrames)
                    {
                        result.Result = _mapper.Map<ScheduleJobModel>(schedule)
                            .WithReceiver(receiverId)
                            .WithTimeFrame(timeFrame)
                            .CalculateStartTime();

                        result.Succeed = true;
                    }
                }
                catch (Exception ex)
                {
                    result.Succeed = false;
                    result.Exception = ex;
                    result.Message = $"Error occurs during calculation of schedule {schedule.Id}";
                }
                results.Add(result);
            }

            return results;
        }
    }
}
