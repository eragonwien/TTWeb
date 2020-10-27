using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class RemovePluralizingTableNameConvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUserPermissionMappings_LoginUsers_LoginUserId",
                table: "LoginUserPermissionMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginUsers",
                table: "LoginUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginUserPermissionMappings",
                table: "LoginUserPermissionMappings");

            migrationBuilder.RenameTable(
                name: "LoginUsers",
                newName: "LoginUser");

            migrationBuilder.RenameTable(
                name: "LoginUserPermissionMappings",
                newName: "LoginUserPermissionMapping");

            migrationBuilder.RenameIndex(
                name: "IX_LoginUsers_Email",
                table: "LoginUser",
                newName: "IX_LoginUser_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginUser",
                table: "LoginUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginUserPermissionMapping",
                table: "LoginUserPermissionMapping",
                columns: new[] { "LoginUserId", "UserPermissionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUserPermissionMapping_LoginUser_LoginUserId",
                table: "LoginUserPermissionMapping",
                column: "LoginUserId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUserPermissionMapping_LoginUser_LoginUserId",
                table: "LoginUserPermissionMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginUserPermissionMapping",
                table: "LoginUserPermissionMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginUser",
                table: "LoginUser");

            migrationBuilder.RenameTable(
                name: "LoginUserPermissionMapping",
                newName: "LoginUserPermissionMappings");

            migrationBuilder.RenameTable(
                name: "LoginUser",
                newName: "LoginUsers");

            migrationBuilder.RenameIndex(
                name: "IX_LoginUser_Email",
                table: "LoginUsers",
                newName: "IX_LoginUsers_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginUserPermissionMappings",
                table: "LoginUserPermissionMappings",
                columns: new[] { "LoginUserId", "UserPermissionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginUsers",
                table: "LoginUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUserPermissionMappings_LoginUsers_LoginUserId",
                table: "LoginUserPermissionMappings",
                column: "LoginUserId",
                principalTable: "LoginUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
