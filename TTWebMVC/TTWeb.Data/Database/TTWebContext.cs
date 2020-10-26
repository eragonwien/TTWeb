using Microsoft.EntityFrameworkCore;
using TTWeb.Data.Models;
using TTWeb.Data.Extensions;

namespace TTWeb.Data.Database
{
    public class TTWebContext : DbContext
    {
        public TTWebContext(DbContextOptions<TTWebContext> options) : base(options)
        {

        }

        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<LoginUserPermissionMapping> LoginUserPermissionMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ConfigureLoginUser()
                .ConfigureUserPermission()
                .ConfigureLoginUserPermissionMapping()
                .ConfigureFacebookUser()
                .ConfigureFacebookUserReceiverMapping()
                .ConfigureScheduleWeekdayMapping()
                .ConfigureTimeFrame()
                .ConfigureSchedule()
                .ConfigureScheduleJob()
                .ConfigureScheduleJobResult();
        }

    }
}
