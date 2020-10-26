using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class RemoveEnumTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUserPermissionMappings_UserPermissions_UserPermissionId",
                table: "LoginUserPermissionMappings");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_LoginUserPermissionMappings_UserPermissionId",
                table: "LoginUserPermissionMappings");

            migrationBuilder.AddColumn<int>(
                name: "UserPermission",
                table: "LoginUserPermissionMappings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPermission",
                table: "LoginUserPermissionMappings");

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoginUserPermissionMappings_UserPermissionId",
                table: "LoginUserPermissionMappings",
                column: "UserPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUserPermissionMappings_UserPermissions_UserPermissionId",
                table: "LoginUserPermissionMappings",
                column: "UserPermissionId",
                principalTable: "UserPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
