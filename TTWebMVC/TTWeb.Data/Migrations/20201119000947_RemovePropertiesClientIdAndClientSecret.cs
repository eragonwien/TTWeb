using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class RemovePropertiesClientIdAndClientSecret : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "LoginUser");

            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "LoginUser");

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
                value: new DateTime(2020, 11, 19, 0, 9, 47, 115, DateTimeKind.Utc).AddTicks(5283));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "LoginUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "LoginUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "LoginUser",
                type: "int",
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
                value: new DateTime(2020, 11, 18, 23, 43, 18, 726, DateTimeKind.Utc).AddTicks(6872));
        }
    }
}
