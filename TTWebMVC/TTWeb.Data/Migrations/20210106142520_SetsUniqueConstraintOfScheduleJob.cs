using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class SetsUniqueConstraintOfScheduleJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ScheduleJob_ScheduleId",
                table: "ScheduleJob");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJob_ScheduleId_StartDate_Action_ReceiverId_SenderId",
                table: "ScheduleJob",
                columns: new[] { "ScheduleId", "StartDate", "Action", "ReceiverId", "SenderId" },
                unique: true,
                filter: "[StartDate] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ScheduleJob_ScheduleId_StartDate_Action_ReceiverId_SenderId",
                table: "ScheduleJob");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJob_ScheduleId",
                table: "ScheduleJob",
                column: "ScheduleId");
        }
    }
}
