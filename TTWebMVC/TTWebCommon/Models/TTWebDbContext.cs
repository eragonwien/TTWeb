﻿using Microsoft.EntityFrameworkCore;
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
      public DbSet<ScheduleJob> ScheduleJobSet { get; set; }
      public DbSet<ScheduleJobType> ScheduleJobTypeSet { get; set; }
      public DbSet<ScheduleJobParameter> ScheduleJobParameterSet { get; set; }
      public DbSet<ScheduleJobParameterType> ScheduleJobParameterTypeSet { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<AppUserRole>()
            .HasKey(rm => new { rm.AppUserId, rm.RoleId });

         modelBuilder.Entity<AppUserRole>()
            .HasOne(rm => rm.AppUser)
            .WithMany(u => u.AppUserRoles)
            .HasForeignKey(rm => rm.AppUserId);

         modelBuilder.Entity<AppUserRole>()
            .HasOne(rm => rm.Role)
            .WithMany(u => u.AppUserRoles)
            .HasForeignKey(rm => rm.AppUserId);

         modelBuilder.Entity<Role>()
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
