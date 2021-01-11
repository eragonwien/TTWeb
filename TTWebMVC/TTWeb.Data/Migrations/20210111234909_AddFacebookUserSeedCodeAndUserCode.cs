using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddFacebookUserSeedCodeAndUserCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "FacebookUser");

            migrationBuilder.DropColumn(
                name: "ProfileAddress",
                table: "FacebookUser");

            migrationBuilder.AddColumn<string>(
                name: "SeedCode",
                table: "FacebookUser",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCode",
                table: "FacebookUser",
                maxLength: 64,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockAt", "LockedUntil" },
                values: new object[] { new DateTime(2021, 1, 11, 23, 49, 8, 368, DateTimeKind.Utc).AddTicks(1513), new DateTime(2021, 1, 11, 23, 54, 8, 368, DateTimeKind.Utc).AddTicks(1550) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeedCode",
                table: "FacebookUser");

            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "FacebookUser");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "FacebookUser",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileAddress",
                table: "FacebookUser",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LockAt", "LockedUntil" },
                values: new object[] { new DateTime(2021, 1, 7, 11, 4, 59, 829, DateTimeKind.Utc).AddTicks(9233), new DateTime(2021, 1, 7, 11, 9, 59, 829, DateTimeKind.Utc).AddTicks(9731) });
        }
    }
}
