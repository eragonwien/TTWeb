using Microsoft.EntityFrameworkCore;
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
        public DbSet<LoginUserPermissionMapping> LoginUserPermissionMappings { get; set; }

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
    }
}