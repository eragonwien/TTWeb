using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddSenderAndReceiverRelationToScheduleJob : Migration
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
                value: new DateTime(2020, 11, 13, 13, 15, 55, 192, DateTimeKind.Utc).AddTicks(4764));

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJob_ReceiverId",
                table: "ScheduleJob",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJob_SenderId",
                table: "ScheduleJob",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleJob_FacebookUser_ReceiverId",
                table: "ScheduleJob",
                column: "ReceiverId",
                principalTable: "FacebookUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleJob_FacebookUser_SenderId",
                table: "ScheduleJob",
                column: "SenderId",
                principalTable: "FacebookUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleJob_FacebookUser_ReceiverId",
                table: "ScheduleJob");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleJob_FacebookUser_SenderId",
                table: "ScheduleJob");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleJob_ReceiverId",
                table: "ScheduleJob");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleJob_SenderId",
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
                value: new DateTime(2020, 11, 13, 13, 12, 58, 899, DateTimeKind.Utc).AddTicks(7275));
        }
    }
}
