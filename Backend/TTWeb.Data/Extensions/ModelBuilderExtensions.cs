using System;
using Microsoft.EntityFrameworkCore;
using TTWeb.Data.Models;

namespace TTWeb.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        private const int MaxLengthMediumString = 64;
        private const int MaxLengthLongString = 128;

        #region Configurations

        public static ModelBuilder RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
                entity.SetTableName(entity.DisplayName());

            return modelBuilder;
        }

        #endregion

        #region Models

        public static ModelBuilder ConfigureLoginUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUser>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<LoginUser>()
                .Property(m => m.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<LoginUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<LoginUser>()
                .Property(e => e.Email)
                .HasMaxLength(MaxLengthLongString)
                .IsRequired();

            modelBuilder.Entity<LoginUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(MaxLengthLongString)
                .IsRequired();

            modelBuilder.Entity<LoginUser>()
                .Property(e => e.LastName)
                .HasMaxLength(MaxLengthLongString)
                .IsRequired();

            modelBuilder.Entity<LoginUser>()
                .HasMany(e => e.OwnedFacebookUsers)
                .WithOne(m => m.Owner)
                .HasForeignKey(m => m.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoginUser>()
                .HasMany(e => e.OwnedSchedules)
                .WithOne(m => m.Owner)
                .HasForeignKey(m => m.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

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
                .OnDelete(DeleteBehavior.Cascade);

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
                .UseIdentityColumn();

            modelBuilder.Entity<FacebookUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<FacebookUser>()
                .Property(u => u.Username)
                .HasMaxLength(MaxLengthLongString)
                .IsRequired();

            modelBuilder.Entity<FacebookUser>()
                .Property(u => u.Password)
                .HasMaxLength(MaxLengthLongString);

            modelBuilder.Entity<FacebookUser>()
                .Property(u => u.SeedCode)
                .HasMaxLength(MaxLengthLongString);

            modelBuilder.Entity<FacebookUser>()
                .Property(u => u.UserCode)
                .HasMaxLength(MaxLengthMediumString);

            modelBuilder.Entity<FacebookUser>()
                .Property(e => e.OwnerId)
                .IsRequired();

            modelBuilder.Entity<FacebookUser>()
                .Property(e => e.Enabled)
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
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ScheduleReceiverMapping>()
                .HasOne(m => m.Receiver)
                .WithMany(p => p.ScheduleReceiverMappings)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureScheduleWeekdayMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleWeekdayMapping>()
                .HasKey(m => new { m.ScheduleId, m.Weekday });

            modelBuilder.Entity<ScheduleWeekdayMapping>()
                .HasOne(m => m.Schedule)
                .WithMany(u => u.ScheduleWeekdayMappings)
                .HasForeignKey(m => m.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ScheduleWeekdayMapping>()
                .Property(m => m.Weekday)
                .HasMaxLength(MaxLengthMediumString)
                .HasConversion<string>();

            return modelBuilder;
        }

        public static ModelBuilder ConfigureTimeFrame(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleTimeFrame>()
                .HasKey(m => new { m.ScheduleId, m.From, m.To });

            return modelBuilder;
        }

        public static ModelBuilder ConfigureSchedule(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Schedule>()
                .Property(m => m.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Schedule>()
                .Property(e => e.Action)
                .IsRequired()
                .HasMaxLength(MaxLengthMediumString)
                .HasConversion<string>();

            modelBuilder.Entity<Schedule>()
                .Property(e => e.IntervalType)
                .IsRequired()
                .HasMaxLength(MaxLengthMediumString)
                .HasConversion<string>();

            modelBuilder.Entity<Schedule>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SenderSchedules)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Schedule>()
                .HasMany(m => m.TimeFrames)
                .WithOne(tf => tf.Schedule)
                .HasForeignKey(tf => tf.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.OwnerId)
                .IsRequired();

            modelBuilder.Entity<Schedule>()
                .Property(m => m.PlanningStatus)
                .HasColumnName("PlanningStatusId")
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(ProcessingStatus.New);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureScheduleJob(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleJob>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<ScheduleJob>()
                .Property(m => m.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<ScheduleJob>()
                .HasIndex(m => new { m.ScheduleId, m.StartDate, m.Action, m.ReceiverId, m.SenderId })
                .IsUnique();

            modelBuilder.Entity<ScheduleJob>()
                .Property(m => m.MaxRetryCount)
                .HasDefaultValue(5);

            modelBuilder.Entity<ScheduleJob>()
                .Property(m => m.RetryCount)
                .HasDefaultValue(0);

            modelBuilder.Entity<ScheduleJob>()
                .HasOne(m => m.Schedule)
                .WithMany(u => u.ScheduleJobs)
                .HasForeignKey(m => m.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ScheduleJob>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SenderScheduleJobs)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ScheduleJob>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceiverScheduleJobs)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureScheduleJobResult(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleJobResult>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<ScheduleJobResult>()
                .Property(m => m.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<ScheduleJobResult>()
                .HasOne(m => m.ScheduleJob)
                .WithMany(u => u.Results)
                .HasForeignKey(m => m.ScheduleJobId)
                .OnDelete(DeleteBehavior.Cascade);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureConfigurationEntry(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigurationEntry>()
                .HasKey(m => m.Key);

            modelBuilder.Entity<ConfigurationEntry>()
                .Property(m => m.Key)
                .IsRequired();

            return modelBuilder;
        }

        #endregion
    }
}