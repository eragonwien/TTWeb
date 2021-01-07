using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;

namespace TTWeb.Data.Database
{
    public class TTWebContext : DbContext
    {
        public TTWebContext(DbContextOptions<TTWebContext> options) : base(options)
        {
        }

        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<FacebookUser> FacebookUsers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleReceiverMapping> ScheduleReceiverMappings { get; set; }
        public DbSet<ScheduleWeekdayMapping> ScheduleWeekdayMappings { get; set; }
        public DbSet<ScheduleTimeFrame> ScheduleTimeFrames { get; set; }
        public DbSet<ScheduleJob> ScheduleJobs { get; set; }
        public DbSet<ScheduleJobResult> ScheduleJobsResults { get; set; }
        public DbSet<ConfigurationEntry> ConfigurationEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .RemovePluralizingTableNameConvention()
                .ConfigureLoginUser()
                .ConfigureLoginUserPermissionMapping()
                .ConfigureFacebookUser()
                .ConfigureFacebookUserReceiverMapping()
                .ConfigureScheduleWeekdayMapping()
                .ConfigureTimeFrame()
                .ConfigureSchedule()
                .ConfigureScheduleJob()
                .ConfigureScheduleJobResult()
                .ConfigureConfigurationEntry()
                .SeedLoginUser()
                .SeedLoginUserPermissionMapping()
                .SeedFacebookUser()
                .SeedSchedule()
                .SeedFacebookUserReceiverMapping()
                .SeedScheduleWeekdayMapping()
                .SeedTimeFrame()
                .SeedScheduleJob()
                .SeedScheduleJobResult();
        }

        public static void UseDbContext(DbContextOptionsBuilder builder, IConfiguration configuration)
        {
            builder.UseNpgsql(configuration.GetConnectionString("TTWeb"));
        }
    }
}