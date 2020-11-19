using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddsNewTablesWorkerAndWorkerPermissionMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ScheduleJob",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2020, 11, 19, 0, 27, 17, 676, DateTimeKind.Utc).AddTicks(3110));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ScheduleJob",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2020, 11, 19, 0, 9, 47, 115, DateTimeKind.Utc).AddTicks(5283));
        }
    }
}
