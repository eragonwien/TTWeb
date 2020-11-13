using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddSenderAndReceiverIdToScheduleJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "ScheduleJob",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "ScheduleJob",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "ReceiverId", "SenderId", "StartDate" },
                values: new object[] { 1, 1, new DateTime(2020, 11, 13, 13, 12, 58, 899, DateTimeKind.Utc).AddTicks(7275) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "ScheduleJob");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "ScheduleJob");

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
                value: new DateTime(2020, 11, 13, 13, 6, 38, 97, DateTimeKind.Utc).AddTicks(312));
        }
    }
}
