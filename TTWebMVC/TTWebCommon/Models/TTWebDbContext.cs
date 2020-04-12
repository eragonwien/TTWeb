using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebCommon.Models
{
   public class TTWebDbContext : DbContext
   {
      public TTWebDbContext(DbContextOptions options) : base(options)
      {
      }

      public DbSet<AppUser> AppUserSet { get; set; }
      public DbSet<LoginUser> LoginUserSet { get; set; }
      public DbSet<ScheduleJob> ScheduleJobSet { get; set; }
      public DbSet<ScheduleJobType> ScheduleJobTypeSet { get; set; }
      public DbSet<ScheduleJobParameter> ScheduleJobParameterSet { get; set; }
      public DbSet<ScheduleJobParameterType> ScheduleJobParameterTypeSet { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<LoginUserRoleMapping>()
            .HasKey(rm => new { rm.LoginUserId, rm.LoginUserRoleId });

         modelBuilder.Entity<LoginUserRoleMapping>()
            .HasOne(rm => rm.LoginUser)
            .WithMany(u => u.LoginUserRolesMapping)
            .HasForeignKey(rm => rm.LoginUserId);

         modelBuilder.Entity<LoginUserRoleMapping>()
            .HasOne(rm => rm.LoginUserRole)
            .WithMany(u => u.LoginUsersRoleMapping)
            .HasForeignKey(rm => rm.LoginUserId);

         modelBuilder.Entity<LoginUserRole>()
            .Property(r => r.Name)
            .HasConversion(r => r.ToString(), r => (LoginUserRoleEnum)Enum.Parse(typeof(LoginUserRoleEnum), r));

         modelBuilder.Entity<AppUser>()
            .HasMany(a => a.ScheduleJobs)
            .WithOne(j => j.AppUser)
            .HasForeignKey(j => j.AppUserId);

         modelBuilder.Entity<ScheduleJob>()
            .HasMany(j => j.Parameters)
            .WithOne(p => p.Job)
            .HasForeignKey(p => p.ScheduleJobId);
      }
   }
}
