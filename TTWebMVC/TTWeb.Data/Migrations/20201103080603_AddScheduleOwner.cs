using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddScheduleOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Schedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_OwnerId",
                table: "Schedule",
                column: "OwnerId");

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

            migrationBuilder.DropIndex(
                name: "IX_Schedule_OwnerId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Schedule");
        }
    }
}
