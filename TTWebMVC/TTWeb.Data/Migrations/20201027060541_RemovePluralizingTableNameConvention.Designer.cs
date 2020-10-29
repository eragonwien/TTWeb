﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTWeb.Data.Database;

namespace TTWeb.Data.Migrations
{
    [DbContext(typeof(TTWebContext))]
    [Migration("20201027060541_RemovePluralizingTableNameConvention")]
    partial class RemovePluralizingTableNameConvention
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TTWeb.Data.Models.FacebookUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<int>("SendScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("FacebookUser");
                });

            modelBuilder.Entity("TTWeb.Data.Models.LoginUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("LoginUser");
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
                });

            modelBuilder.Entity("TTWeb.Data.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("IntervalType")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScheduleJob");
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJobResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ScheduleJobId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleJobId");

                    b.ToTable("ScheduleJobResult");
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
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleWeekdayMapping", b =>
                {
                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("Weekday")
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.HasKey("ScheduleId", "Weekday");

                    b.ToTable("ScheduleWeekdayMapping");
                });

            modelBuilder.Entity("TTWeb.Data.Models.TimeFrame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("From")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("To")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("TimeFrame");
                });

            modelBuilder.Entity("TTWeb.Data.Models.LoginUserPermissionMapping", b =>
                {
                    b.HasOne("TTWeb.Data.Models.LoginUser", "LoginUser")
                        .WithMany("LoginUserPermissionMappings")
                        .HasForeignKey("LoginUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.Schedule", b =>
                {
                    b.HasOne("TTWeb.Data.Models.FacebookUser", "Sender")
                        .WithMany("SendSchedule")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJob", b =>
                {
                    b.HasOne("TTWeb.Data.Models.Schedule", "Schedule")
                        .WithMany("ScheduleJobs")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleJobResult", b =>
                {
                    b.HasOne("TTWeb.Data.Models.ScheduleJob", "ScheduleJob")
                        .WithMany("Results")
                        .HasForeignKey("ScheduleJobId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleReceiverMapping", b =>
                {
                    b.HasOne("TTWeb.Data.Models.FacebookUser", "Receiver")
                        .WithMany("ScheduleReceiverMappings")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TTWeb.Data.Models.Schedule", "Schedule")
                        .WithMany("ScheduleReceiverMappings")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.ScheduleWeekdayMapping", b =>
                {
                    b.HasOne("TTWeb.Data.Models.Schedule", "Schedule")
                        .WithMany("ScheduleWeekdayMappings")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TTWeb.Data.Models.TimeFrame", b =>
                {
                    b.HasOne("TTWeb.Data.Models.Schedule", "Schedule")
                        .WithMany("TimeFrames")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}