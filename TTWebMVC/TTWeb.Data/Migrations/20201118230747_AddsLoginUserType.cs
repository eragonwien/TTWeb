using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddsLoginUserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "LoginUser",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "LoginUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "TypeId",
                value: 1);

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
                value: new DateTime(2020, 11, 18, 23, 7, 47, 130, DateTimeKind.Utc).AddTicks(4714));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "LoginUser");

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
                value: new DateTime(2020, 11, 13, 13, 15, 55, 192, DateTimeKind.Utc).AddTicks(4764));
        }
    }
}
