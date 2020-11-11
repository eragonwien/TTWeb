using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class FixOwnerMultipleCascadePathProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "FacebookUser",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "FacebookUser",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "FacebookUser",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "FacebookUser",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileAddress",
                table: "FacebookUser",
                maxLength: 128,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "FacebookUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "Enabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule",
                column: "OwnerId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "FacebookUser");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "FacebookUser");

            migrationBuilder.DropColumn(
                name: "ProfileAddress",
                table: "FacebookUser");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "FacebookUser",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "FacebookUser",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule",
                column: "OwnerId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
