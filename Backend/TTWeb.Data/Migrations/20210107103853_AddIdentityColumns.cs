using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddIdentityColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockAt", "LockedUntil" },
                values: new object[] { new DateTime(2021, 1, 7, 10, 38, 52, 407, DateTimeKind.Utc).AddTicks(2977), new DateTime(2021, 1, 7, 10, 43, 52, 407, DateTimeKind.Utc).AddTicks(3680) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockAt", "LockedUntil" },
                values: new object[] { new DateTime(2021, 1, 7, 10, 31, 37, 650, DateTimeKind.Utc).AddTicks(9421), new DateTime(2021, 1, 7, 10, 36, 37, 651, DateTimeKind.Utc).AddTicks(44) });
        }
    }
}
