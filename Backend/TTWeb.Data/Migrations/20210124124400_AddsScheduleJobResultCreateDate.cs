using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddsScheduleJobResultCreateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ScheduleJobResult",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ScheduleJobResult");
        }
    }
}
