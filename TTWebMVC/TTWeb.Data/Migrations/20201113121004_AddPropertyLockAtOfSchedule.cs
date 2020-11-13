using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddPropertyLockAtOfSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LockAt",
                table: "Schedule",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockAt",
                table: "Schedule");

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);
        }
    }
}
