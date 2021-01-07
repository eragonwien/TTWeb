using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class SetConfigurationEntryKeyRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockAt", "LockedUntil" },
                values: new object[] { new DateTime(2021, 1, 7, 11, 4, 59, 829, DateTimeKind.Utc).AddTicks(9233), new DateTime(2021, 1, 7, 11, 9, 59, 829, DateTimeKind.Utc).AddTicks(9731) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockAt", "LockedUntil" },
                values: new object[] { new DateTime(2021, 1, 7, 11, 0, 45, 324, DateTimeKind.Utc).AddTicks(2001), new DateTime(2021, 1, 7, 11, 5, 45, 324, DateTimeKind.Utc).AddTicks(2486) });
        }
    }
}
