﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
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
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TTWeb.Data.Models.ConfigurationEntry", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Key");

                    b.ToTable("ConfigurationEntry");
                });

            modelBuilder.Entity("TTWeb.Data.Models.FacebookUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("SeedCode")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("UserCode")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("FacebookUser");
                });

            modelBuilder.Entity("TTWeb.Data.Models.LoginUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("LoginUser");
                });

            modelBuilder.Entity("TTWeb.Data.Models.LoginUserPermissionMapping", b =>
                {
                    b.Property<int>("LoginUserId")
                        .HasColumnType("integer");

                    b.Property<int>("UserPermission")
                        .HasColumnName("UserPermissionId")
                        .HasColumnType("integer");

                    b.HasKey("LoginUserId", "UserPermission");

                    b.ToTable("LoginUserPermissionMapping");
                });

            modelBuilder.Entity("TTWeb.Data.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IntervalType")
                        .IsRequired()
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.Property<DateTime?>("LockAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LockedUntil")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("PlanningStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PlanningStatusId")
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<int?>("SenderId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SenderId");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Action")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LockAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LockedUntil")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MaxRetryCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(5);

                    b.Property<int>("ReceiverId")
                        .HasColumnType("integer");

                    b.Property<int>("RetryCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("ScheduleId")
                        .HasColumnType("integer");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("ScheduleId", "StartDate", "Action", "ReceiverId", "SenderId")
                        .IsUnique();

                    b.ToTable("ScheduleJob");
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJobResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<int>("ScheduleJobId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleJobId");

                    b.ToTable("ScheduleJobResult");
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleReceiverMapping", b =>
                {
                    b.Property<int>("ScheduleId")
                        .HasColumnType("integer");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("integer");

                    b.HasKey("ScheduleId", "ReceiverId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("ScheduleReceiverMapping");
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleTimeFrame", b =>
                {
                    b.Property<int>("ScheduleId")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("From")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("To")
                        .HasColumnType("interval");

                    b.HasKey("ScheduleId", "From", "To");

                    b.ToTable("ScheduleTimeFrame");
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleWeekdayMapping", b =>
                {
                    b.Property<int>("ScheduleId")
                        .HasColumnType("integer");

                    b.Property<string>("Weekday")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.HasKey("ScheduleId", "Weekday");

                    b.ToTable("ScheduleWeekdayMapping");
                });

            modelBuilder.Entity("TTWeb.Data.Models.FacebookUser", b =>
                {
                    b.HasOne("TTWeb.Data.Models.LoginUser", "Owner")
                        .WithMany("OwnedFacebookUsers")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
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
                        .WithMany("SenderSchedules")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJob", b =>
                {
                    b.HasOne("TTWeb.Data.Models.FacebookUser", "Receiver")
                        .WithMany("ReceiverScheduleJobs")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
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
