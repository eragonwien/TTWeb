using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class ChangesPropertyOfScheduleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);
        }
    }
}
