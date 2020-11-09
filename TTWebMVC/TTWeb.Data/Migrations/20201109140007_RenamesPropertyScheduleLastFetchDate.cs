using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class RenamesPropertyScheduleLastFetchDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlannedDate",
                table: "Schedule");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastFetchDate",
                table: "Schedule",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastFetchDate",
                table: "Schedule");

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedDate",
                table: "Schedule",
                type: "datetime2",
                nullable: true);
        }
    }
}
