using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddsScheduleJobRetryCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxRetryCount",
                table: "ScheduleJob",
                nullable: false,
                defaultValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "RetryCount",
                table: "ScheduleJob",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockAt", "LockedUntil" },
                values: new object[] { new DateTime(2021, 1, 12, 23, 46, 22, 671, DateTimeKind.Utc).AddTicks(6961), new DateTime(2021, 1, 12, 23, 51, 22, 671, DateTimeKind.Utc).AddTicks(6986) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxRetryCount",
                table: "ScheduleJob");

            migrationBuilder.DropColumn(
                name: "RetryCount",
                table: "ScheduleJob");

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockAt", "LockedUntil" },
                values: new object[] { new DateTime(2021, 1, 11, 23, 49, 8, 368, DateTimeKind.Utc).AddTicks(1513), new DateTime(2021, 1, 11, 23, 54, 8, 368, DateTimeKind.Utc).AddTicks(1550) });
        }
    }
}
