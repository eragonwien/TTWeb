using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddAutoGenenaredId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleWeekdayMapping_Weekday_WeekdayId",
                table: "ScheduleWeekdayMapping");

            migrationBuilder.DropTable(
                name: "Weekday");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleWeekdayMapping_WeekdayId",
                table: "ScheduleWeekdayMapping");

            migrationBuilder.AddColumn<int>(
                name: "Weekday",
                table: "ScheduleWeekdayMapping",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weekday",
                table: "ScheduleWeekdayMapping");

            migrationBuilder.CreateTable(
                name: "Weekday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weekday", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleWeekdayMapping_WeekdayId",
                table: "ScheduleWeekdayMapping",
                column: "WeekdayId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleWeekdayMapping_Weekday_WeekdayId",
                table: "ScheduleWeekdayMapping",
                column: "WeekdayId",
                principalTable: "Weekday",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
