using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class ChangePrimaryKeyOfScheduleTimeFrame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacebookUser_LoginUser_OwnerId",
                table: "FacebookUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleTimeFrame",
                table: "ScheduleTimeFrame");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleTimeFrame_ScheduleId",
                table: "ScheduleTimeFrame");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ScheduleTimeFrame");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleTimeFrame",
                table: "ScheduleTimeFrame",
                columns: new[] { "ScheduleId", "From", "To" });

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_FacebookUser_LoginUser_OwnerId",
                table: "FacebookUser",
                column: "OwnerId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule",
                column: "OwnerId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacebookUser_LoginUser_OwnerId",
                table: "FacebookUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleTimeFrame",
                table: "ScheduleTimeFrame");

            migrationBuilder.DeleteData(
                table: "ScheduleTimeFrame",
                keyColumns: new[] { "ScheduleId", "From", "To" },
                keyValues: new object[] { 1, new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0) });

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ScheduleTimeFrame",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleTimeFrame",
                table: "ScheduleTimeFrame",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);

            migrationBuilder.InsertData(
                table: "ScheduleTimeFrame",
                columns: new[] { "Id", "From", "ScheduleId", "To" },
                values: new object[] { 1, new TimeSpan(0, 9, 0, 0, 0), 1, new TimeSpan(0, 14, 0, 0, 0) });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTimeFrame_ScheduleId",
                table: "ScheduleTimeFrame",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacebookUser_LoginUser_OwnerId",
                table: "FacebookUser",
                column: "OwnerId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule",
                column: "OwnerId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
