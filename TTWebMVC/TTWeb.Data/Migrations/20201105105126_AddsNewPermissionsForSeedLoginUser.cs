using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddsNewPermissionsForSeedLoginUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LoginUserPermissionMapping",
                columns: new[] { "LoginUserId", "UserPermissionId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "LoginUserPermissionMapping",
                columns: new[] { "LoginUserId", "UserPermissionId" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "LoginUserPermissionMapping",
                columns: new[] { "LoginUserId", "UserPermissionId" },
                values: new object[] { 1, 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LoginUserPermissionMapping",
                keyColumns: new[] { "LoginUserId", "UserPermissionId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "LoginUserPermissionMapping",
                keyColumns: new[] { "LoginUserId", "UserPermissionId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "LoginUserPermissionMapping",
                keyColumns: new[] { "LoginUserId", "UserPermissionId" },
                keyValues: new object[] { 1, 4 });
        }
    }
}
