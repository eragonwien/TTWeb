using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddNewRoleForSeedLoginUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LoginUserPermissionMapping",
                columns: new[] { "LoginUserId", "UserPermissionId" },
                values: new object[] { 1, 6 });

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LoginUserPermissionMapping",
                keyColumns: new[] { "LoginUserId", "UserPermissionId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);
        }
    }
}
