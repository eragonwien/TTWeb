using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
      public DbSet<Role> RoleSet { get; set; }
      public DbSet<ScheduleJobDef> ScheduleJobDefSet { get; set; }
      public DbSet<ScheduleJobDetail> ScheduleJobDetailSet { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         #region AppUserRole

         modelBuilder.Entity<AppUserRole>()
           .HasKey(rm => new { rm.AppUserId, rm.RoleId });

         modelBuilder.Entity<AppUserRole>()
            .HasOne(rm => rm.AppUser)
            .WithMany(u => u.AppUserRoles)
            .HasForeignKey(rm => rm.AppUserId);

         modelBuilder.Entity<AppUserRole>()
            .HasOne(rm => rm.Role)
            .WithMany(u => u.AppUserRoles)
            .HasForeignKey(rm => rm.RoleId);

         modelBuilder.Entity<Role>()
            .Property(r => r.Name)
            .HasConversion(r => r.ToString(), r => (LoginUserRoleEnum)Enum.Parse(typeof(LoginUserRoleEnum), r));

         #endregion

         #region JobWeekDay

         modelBuilder.Entity<JobWeekDay>()
           .HasKey(rm => new { rm.ScheduleJobDefId, rm.WeekDayId });

         modelBuilder.Entity<JobWeekDay>()
            .HasOne(rm => rm.ScheduleJobDef)
            .WithMany(u => u.JobWeekDays)
            .HasForeignKey(rm => rm.ScheduleJobDefId);

         modelBuilder.Entity<JobWeekDay>()
            .HasOne(rm => rm.WeekDay)
            .WithMany(u => u.JobWeekDays)
            .HasForeignKey(rm => rm.WeekDayId);

         modelBuilder.Entity<WeekDay>()
            .Property(r => r.Name)
            .HasConversion(r => r.ToString(), r => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), r));

         #endregion

         #region Partner

         modelBuilder.Entity<ScheduleJobPartner>()
           .HasKey(rm => new { rm.ScheduleJobDefId, rm.PartnerId });

         modelBuilder.Entity<ScheduleJobPartner>()
            .HasOne(rm => rm.ScheduleJobDef)
            .WithMany(u => u.ScheduleJobPartners)
            .HasForeignKey(rm => rm.ScheduleJobDefId);

         modelBuilder.Entity<ScheduleJobPartner>()
            .HasOne(rm => rm.Partner)
            .WithMany(u => u.ScheduleJobPartners)
            .HasForeignKey(rm => rm.PartnerId);

         #endregion

         #region ScheduleJobDef

         modelBuilder.Entity<ScheduleJobDef>()
            .Property(r => r.Type)
            .HasConversion(r => r.ToString(), r => (ScheduleJobType)Enum.Parse(typeof(ScheduleJobType), r));

         modelBuilder.Entity<ScheduleJobDef>()
            .Property(r => r.IntervalType)
            .HasConversion(r => r.ToString(), r => (IntervalType)Enum.Parse(typeof(IntervalType), r));

         #endregion

         #region ScheduleJobDetail

         modelBuilder.Entity<ScheduleJobDetail>()
            .Property(r => r.Status)
            .HasConversion(r => r.ToString(), r => (ScheduleJobStatus)Enum.Parse(typeof(ScheduleJobStatus), r));

         #endregion


      }
   }
}
