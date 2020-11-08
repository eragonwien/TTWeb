using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class ChangesScheduleTimeFrameForeignKeyDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleTimeFrame_Schedule_ScheduleId",
                table: "ScheduleTimeFrame");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleTimeFrame_Schedule_ScheduleId",
                table: "ScheduleTimeFrame",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleTimeFrame_Schedule_ScheduleId",
                table: "ScheduleTimeFrame");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleTimeFrame_Schedule_ScheduleId",
                table: "ScheduleTimeFrame",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
