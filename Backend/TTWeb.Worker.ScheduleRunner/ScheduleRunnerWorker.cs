using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.BusinessLogic.Services;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;
using TTWeb.Worker.Core;
using TTWeb.Worker.ScheduleRunner.Services;

namespace TTWeb.Worker.ScheduleRunner
{
    public class ScheduleRunnerWorker : BaseWorker
    {
        private readonly IFacebookAutomationService _facebookService;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly ILogger<ScheduleRunnerWorker> _logger;
        private readonly IMapper _mapper;
        private readonly SchedulingAppSettings _schedulingAppSettings;

        public ScheduleRunnerWorker(ILogger<ScheduleRunnerWorker> logger,
            IOptions<SchedulingAppSettings> schedulingAppSettingsOptions,
            IFacebookAutomationService facebookService,
            IServiceScopeFactory scopeFactory,
            IMapper mapper,
            IEncryptionHelper encryptionHelper)
            : base(scopeFactory)
        {
            _logger = logger;
            _schedulingAppSettings = schedulingAppSettingsOptions.Value;
            _facebookService = facebookService;
            _mapper = mapper;
            _encryptionHelper = encryptionHelper;
        }

        protected override async Task DoContinuousWorkAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker running at: {DateTime.UtcNow}");

            var queue = await EnqueueLockJobsAsync(cancellationToken);
            _logger.LogInformation($"{queue.Count} entries were enqueued");

            while (queue.TryDequeue(out var job) && !cancellationToken.IsCancellationRequested)
                await ProcessJobAsync(job, cancellationToken);

            _logger.LogInformation($"Worker completed the queue of {queue.Count} entries at: {DateTime.UtcNow}");
            _logger.LogInformation($"Worker restarts in {_schedulingAppSettings.Job.TriggerInterval.TotalSeconds}s.");
            await Task.Delay(_schedulingAppSettings.Job.TriggerInterval, cancellationToken);
        }

        private async Task<Queue<ScheduleJob>> EnqueueLockJobsAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.ServiceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<TTWebContext>();
            var jobs = await context.ScheduleJobs
                .Include(j => j.Sender)
                .Include(j => j.Receiver)
                .FilterOpenJobs()
                .ToListAsync(cancellationToken);

            var now = DateTime.UtcNow;
            jobs.ForEach(j => j.Lock(now, _schedulingAppSettings.Job.LockDuration));
            await context.SaveChangesAsync(cancellationToken);

            return new Queue<ScheduleJob>(jobs);
        }

        private async Task ProcessJobAsync(ScheduleJob job, CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.ServiceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<TTWebContext>();
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var scheduleJobModel = _mapper.Map<ScheduleJobModel>(job);
            scheduleJobModel.Sender.Password = _encryptionHelper.Decrypt(scheduleJobModel.Sender.Password);
            scheduleJobModel.Receiver.Password = _encryptionHelper.Decrypt(scheduleJobModel.Receiver.Password);

            var result = await _facebookService.ProcessAsync(scheduleJobModel, cancellationToken);

            await UpdateStatusAsync(context, job, result, cancellationToken);
            await CreateScheduleJobResultAsync(context, job, result, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        private async Task UpdateStatusAsync(
            TTWebContext context,
            ScheduleJob job,
            ProcessingResult<ScheduleJobModel> result,
            CancellationToken cancellationToken)
        {
            if (job == null) throw new ArgumentNullException(nameof(job));
            if (result == null) throw new ArgumentNullException(nameof(result));

            try
            {
                context.ScheduleJobs.Attach(job);

                if (result.Succeed)
                {
                    job.Status = ProcessingStatus.Completed;
                    job.RetryCount = 0;
                }
                else
                {
                    job.RetryCount++;
                    job.Status = job.RetryCount < job.MaxRetryCount ? ProcessingStatus.Retry : ProcessingStatus.Error;
                }

                job.Status = result.Succeed ? ProcessingStatus.Completed : ProcessingStatus.Retry;
                job.EndTime = DateTime.UtcNow;

                await context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private static async Task CreateScheduleJobResultAsync(
            TTWebContext context,
            ScheduleJob job,
            ProcessingResult<ScheduleJobModel> result,
            CancellationToken cancellationToken)
        {
            if (job == null) throw new ArgumentNullException(nameof(job));
            if (result is null) throw new ArgumentNullException(nameof(result));

            var jobResult = new ScheduleJobResult
            {
                ScheduleJobId = job.Id,
                Status = job.Status,
                Message = result.Message
            };

            await context.ScheduleJobsResults.AddAsync(jobResult, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}