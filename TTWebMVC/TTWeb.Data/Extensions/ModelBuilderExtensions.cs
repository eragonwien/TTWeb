using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using TTWeb.Data.Models;

namespace TTWeb.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        private const int _maxLengthMediumString = 64;
        private const int _maxLengthLongtring = 256;

        public static ModelBuilder ConfigureLoginUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUser>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<LoginUser>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<LoginUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<LoginUser>()
                .Property(e => e.Email)
                .HasMaxLength(_maxLengthMediumString)
                .IsRequired();

            modelBuilder.Entity<LoginUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(_maxLengthMediumString)
                .IsRequired();

            modelBuilder.Entity<LoginUser>()
                .Property(e => e.LastName)
                .HasMaxLength(_maxLengthMediumString)
                .IsRequired();

            return modelBuilder;
        }

        public static ModelBuilder ConfigureLoginUserPermissionMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUserPermissionMapping>()
                  .HasKey(m => new { m.LoginUserId, m.UserPermission });

            modelBuilder.Entity<LoginUserPermissionMapping>()
               .HasOne(m => m.LoginUser)
               .WithMany(u => u.LoginUserPermissionMappings)
               .HasForeignKey(m => m.LoginUserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoginUserPermissionMapping>()
                .Property(m => m.UserPermission)
                .HasColumnName("UserPermissionId")
                .HasConversion<int>();

            return modelBuilder;
        }

        public static ModelBuilder ConfigureFacebookUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FacebookUser>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<FacebookUser>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<FacebookUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<FacebookUser>()
                .Property(u => u.Username)
                .HasMaxLength(_maxLengthMediumString)
                .IsRequired();

            modelBuilder.Entity<FacebookUser>()
                .Property(u => u.Password)
                .HasMaxLength(_maxLengthMediumString)
                .IsRequired();

            return modelBuilder;
        }

        public static ModelBuilder ConfigureFacebookUserReceiverMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleReceiverMapping>()
                  .HasKey(m => new { m.ScheduleId, m.ReceiverId });

            modelBuilder.Entity<ScheduleReceiverMapping>()
               .HasOne(m => m.Schedule)
               .WithMany(u => u.ScheduleReceiverMappings)
               .HasForeignKey(m => m.ScheduleId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ScheduleReceiverMapping>()
               .HasOne(m => m.Receiver)
               .WithMany(p => p.ScheduleReceiverMappings)
               .HasForeignKey(m => m.ReceiverId)
               .OnDelete(DeleteBehavior.Restrict);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureScheduleWeekdayMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleWeekdayMapping>()
                  .HasKey(m => new { m.ScheduleId, m.WeekdayId });

            modelBuilder.Entity<ScheduleWeekdayMapping>()
               .HasOne(m => m.Schedule)
               .WithMany(u => u.ScheduleWeekdayMappings)
               .HasForeignKey(m => m.ScheduleId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ScheduleWeekdayMapping>()
               .Property(m => m.WeekdayId)
               .HasConversion<int>();

            return modelBuilder;
        }

        public static ModelBuilder ConfigureTimeFrame(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeFrame>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<TimeFrame>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TimeFrame>()
               .HasOne(m => m.Schedule)
               .WithMany(u => u.TimeFrames)
               .HasForeignKey(m => m.ScheduleId)
               .OnDelete(DeleteBehavior.Restrict);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureSchedule(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Schedule>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Schedule>()
                .Property(e => e.Action)
                .IsRequired()
                .HasConversion<string>();

            modelBuilder.Entity<Schedule>()
                .Property(e => e.IntervalType)
                .IsRequired()
                .HasConversion<string>();

            modelBuilder.Entity<Schedule>()
               .HasOne(m => m.Sender)
               .WithMany(u => u.SendSchedule)
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.Restrict);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureScheduleJob(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleJob>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<ScheduleJob>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ScheduleJob>()
               .HasOne(m => m.Schedule)
               .WithMany(u => u.ScheduleJobs)
               .HasForeignKey(m => m.ScheduleId)
               .OnDelete(DeleteBehavior.Restrict);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureScheduleJobResult(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleJobResult>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<ScheduleJobResult>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ScheduleJobResult>()
               .HasOne(m => m.ScheduleJob)
               .WithMany(u => u.Results)
               .HasForeignKey(m => m.ScheduleJobId)
               .OnDelete(DeleteBehavior.Restrict);

            return modelBuilder;
        }
    }
}
