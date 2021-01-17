using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddsScheduleJobResultMessageFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "ScheduleJobResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ScheduleJobResult",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ScheduleJobResult",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "ScheduleJobResult");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "ScheduleJobResult");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ScheduleJobResult");
        }
    }
}
