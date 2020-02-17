using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebNetCommon.Models
{
   public class TTWebDbContext : DbContext
   {
      public TTWebDbContext() : base("TTWebContext")
      {
      }

      public DbSet<AppUser> AppUserSet { get; set; }
      public DbSet<ScheduleJob> ScheduleJobSet { get; set; }
      public DbSet<ScheduleJobType> ScheduleJobTypeSet { get; set; }
      public DbSet<ScheduleJobParameter> ScheduleJobParameterSet { get; set; }
      public DbSet<ScheduleJobParameterType> ScheduleJobParameterTypeSet { get; set; }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
         modelBuilder.Entity<AppUser>()
            .HasMany(a => a.ScheduleJobs)
            .WithRequired(j => j.AppUser)
            .HasForeignKey(j => j.AppUserId);
      }
   }
}
