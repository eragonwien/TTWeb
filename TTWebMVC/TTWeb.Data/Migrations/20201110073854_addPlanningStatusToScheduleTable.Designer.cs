﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTWeb.Data.Database;

namespace TTWeb.Data.Migrations
{
    [DbContext(typeof(TTWebContext))]
    [Migration("20201110073854_addPlanningStatusToScheduleTable")]
    partial class addPlanningStatusToScheduleTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("FacebookUser");

                    b.HasData(
                        new
                        {
                            Id = 1,
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
                            LastName = "dev"
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

                    b.Property<DateTime?>("LockedUntil")
                        .HasColumnType("datetime2");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("PlanningStatus")
                        .HasColumnName("PlanningStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SenderId");

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

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScheduleJob");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ScheduleId = 1
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("From")
                        .HasColumnType("time");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("To")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScheduleTimeFrame");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            From = new TimeSpan(0, 9, 0, 0, 0),
                            ScheduleId = 1,
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTWeb.Data.Models.FacebookUser", "Sender")
                        .WithMany("SendSchedule")
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJob", b =>
                {
                    b.HasOne("TTWeb.Data.Models.Schedule", "Schedule")
                        .WithMany("ScheduleJobs")
                        .HasForeignKey("ScheduleId")
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
