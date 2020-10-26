using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class RemoveColumnUserPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPermission",
                table: "LoginUserPermissionMappings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserPermission",
                table: "LoginUserPermissionMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
