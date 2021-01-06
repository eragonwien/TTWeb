using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddsScheduleJobStartTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ScheduleJob");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "ScheduleJob",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "ScheduleJob",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ScheduleJob");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "ScheduleJob");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ScheduleJob",
                type: "datetime2",
                nullable: true);
        }
    }
}
