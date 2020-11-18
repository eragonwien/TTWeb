using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddsLoginUserClientIdAndSecret : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "LoginUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "LoginUser",
                nullable: true);

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
                value: new DateTime(2020, 11, 18, 23, 23, 51, 250, DateTimeKind.Utc).AddTicks(1692));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "LoginUser");

            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "LoginUser");

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
    }
}
