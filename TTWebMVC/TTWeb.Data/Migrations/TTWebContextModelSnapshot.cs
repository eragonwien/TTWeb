﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTWeb.Data.Database;

namespace TTWeb.Data.Migrations
{
    [DbContext(typeof(TTWebContext))]
    partial class TTWebContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TTWeb.Data.Models.FacebookUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("HomeAddress")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProfileAddress")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("FacebookUser");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Enabled = true,
                            OwnerId = 1,
                            Password = "1234",
                            Username = "eragonwien@gmail.com"
                        });
                });

            modelBuilder.Entity("TTWeb.Data.Models.LoginUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<int>("Type")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TypeId")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("LoginUser");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "test@test.com",
                            FirstName = "test",
                            LastName = "dev",
                            Type = 1
                        });
                });

            modelBuilder.Entity("TTWeb.Data.Models.LoginUserPermissionMapping", b =>
                {
                    b.Property<int>("LoginUserId")
                        .HasColumnType("int");

                    b.Property<int>("UserPermission")
                        .HasColumnName("UserPermissionId")
                        .HasColumnType("int");

                    b.HasKey("LoginUserId", "UserPermission");

                    b.ToTable("LoginUserPermissionMapping");

                    b.HasData(
                        new
                        {
                            LoginUserId = 1,
                            UserPermission = 1
                        },
                        new
                        {
                            LoginUserId = 1,
                            UserPermission = 2
                        },
                        new
                        {
                            LoginUserId = 1,
                            UserPermission = 3
                        },
                        new
                        {
                            LoginUserId = 1,
                            UserPermission = 4
                        },
                        new
                        {
                            LoginUserId = 1,
                            UserPermission = 6
                        });
                });

            modelBuilder.Entity("TTWeb.Data.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("IntervalType")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<DateTime?>("LockAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LockedUntil")
                        .HasColumnType("datetime2");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("PlanningStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PlanningStatusId")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int?>("SenderId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SenderId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Schedule");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Action = "Like",
                            IntervalType = "Daily",
                            OwnerId = 1,
                            PlanningStatus = 1,
                            SenderId = 1
                        });
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("SenderId");

                    b.ToTable("ScheduleJob");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Action = 1,
                            ReceiverId = 1,
                            ScheduleId = 1,
                            SenderId = 1,
                            StartDate = new DateTime(2020, 11, 18, 23, 7, 47, 130, DateTimeKind.Utc).AddTicks(4714),
                            Status = 5
                        });
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJobResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ScheduleJobId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleJobId");

                    b.ToTable("ScheduleJobResult");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ScheduleJobId = 1
                        });
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleReceiverMapping", b =>
                {
                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.HasKey("ScheduleId", "ReceiverId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("ScheduleReceiverMapping");

                    b.HasData(
                        new
                        {
                            ScheduleId = 1,
                            ReceiverId = 1
                        });
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleTimeFrame", b =>
                {
                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("From")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("To")
                        .HasColumnType("time");

                    b.HasKey("ScheduleId", "From", "To");

                    b.ToTable("ScheduleTimeFrame");

                    b.HasData(
                        new
                        {
                            ScheduleId = 1,
                            From = new TimeSpan(0, 9, 0, 0, 0),
                            To = new TimeSpan(0, 14, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleWeekdayMapping", b =>
                {
                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("Weekday")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("ScheduleId", "Weekday");

                    b.ToTable("ScheduleWeekdayMapping");

                    b.HasData(
                        new
                        {
                            ScheduleId = 1,
                            Weekday = "Monday"
                        },
                        new
                        {
                            ScheduleId = 1,
                            Weekday = "Wednesday"
                        },
                        new
                        {
                            ScheduleId = 1,
                            Weekday = "Friday"
                        });
                });

            modelBuilder.Entity("TTWeb.Data.Models.FacebookUser", b =>
                {
                    b.HasOne("TTWeb.Data.Models.LoginUser", "Owner")
                        .WithMany("OwnedFacebookUsers")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.LoginUserPermissionMapping", b =>
                {
                    b.HasOne("TTWeb.Data.Models.LoginUser", "LoginUser")
                        .WithMany("LoginUserPermissionMappings")
                        .HasForeignKey("LoginUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.Schedule", b =>
                {
                    b.HasOne("TTWeb.Data.Models.LoginUser", "Owner")
                        .WithMany("OwnedSchedules")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TTWeb.Data.Models.FacebookUser", "Sender")
                        .WithMany("SenderSchedules")
                        .HasForeignKey("SenderId")
                        .IsRequired();

                    b.HasOne("TTWeb.Data.Models.LoginUser", "Worker")
                        .WithMany("WorkingSchedules")
                        .HasForeignKey("WorkerId");
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJob", b =>
                {
                    b.HasOne("TTWeb.Data.Models.FacebookUser", "Receiver")
                        .WithMany("ReceiverScheduleJobs")
                        .HasForeignKey("ReceiverId")
                        .IsRequired();

                    b.HasOne("TTWeb.Data.Models.Schedule", "Schedule")
                        .WithMany("ScheduleJobs")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTWeb.Data.Models.FacebookUser", "Sender")
                        .WithMany("SenderScheduleJobs")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJobResult", b =>
                {
                    b.HasOne("TTWeb.Data.Models.ScheduleJob", "ScheduleJob")
                        .WithMany("Results")
                        .HasForeignKey("ScheduleJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleReceiverMapping", b =>
                {
                    b.HasOne("TTWeb.Data.Models.FacebookUser", "Receiver")
                        .WithMany("ScheduleReceiverMappings")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTWeb.Data.Models.Schedule", "Schedule")
                        .WithMany("ScheduleReceiverMappings")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleTimeFrame", b =>
                {
                    b.HasOne("TTWeb.Data.Models.Schedule", "Schedule")
                        .WithMany("TimeFrames")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleWeekdayMapping", b =>
                {
                    b.HasOne("TTWeb.Data.Models.Schedule", "Schedule")
                        .WithMany("ScheduleWeekdayMappings")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
