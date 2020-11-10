using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class changesLastFetchDateToLockedUntil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastFetchDate",
                table: "Schedule");

            migrationBuilder.AddColumn<DateTime>(
                name: "LockedUntil",
                table: "Schedule",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockedUntil",
                table: "Schedule");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastFetchDate",
                table: "Schedule",
                type: "datetime2",
                nullable: true);
        }
    }
}
