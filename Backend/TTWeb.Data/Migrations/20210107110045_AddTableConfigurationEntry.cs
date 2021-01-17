using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddTableConfigurationEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigurationEntry",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationEntry", x => x.Key);
                });

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockAt", "LockedUntil" },
                values: new object[] { new DateTime(2021, 1, 7, 11, 0, 45, 324, DateTimeKind.Utc).AddTicks(2001), new DateTime(2021, 1, 7, 11, 5, 45, 324, DateTimeKind.Utc).AddTicks(2486) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationEntry");

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockAt", "LockedUntil" },
                values: new object[] { new DateTime(2021, 1, 7, 10, 38, 52, 407, DateTimeKind.Utc).AddTicks(2977), new DateTime(2021, 1, 7, 10, 43, 52, 407, DateTimeKind.Utc).AddTicks(3680) });
        }
    }
}
