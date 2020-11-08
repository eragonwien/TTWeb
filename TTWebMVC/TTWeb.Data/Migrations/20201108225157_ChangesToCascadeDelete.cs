using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class ChangesToCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUserPermissionMapping_LoginUser_LoginUserId",
                table: "LoginUserPermissionMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleJob_Schedule_ScheduleId",
                table: "ScheduleJob");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleJobResult_ScheduleJob_ScheduleJobId",
                table: "ScheduleJobResult");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleReceiverMapping_FacebookUser_ReceiverId",
                table: "ScheduleReceiverMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleReceiverMapping_Schedule_ScheduleId",
                table: "ScheduleReceiverMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleWeekdayMapping_Schedule_ScheduleId",
                table: "ScheduleWeekdayMapping");

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Schedule",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUserPermissionMapping_LoginUser_LoginUserId",
                table: "LoginUserPermissionMapping",
                column: "LoginUserId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule",
                column: "OwnerId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleJob_Schedule_ScheduleId",
                table: "ScheduleJob",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleJobResult_ScheduleJob_ScheduleJobId",
                table: "ScheduleJobResult",
                column: "ScheduleJobId",
                principalTable: "ScheduleJob",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleReceiverMapping_FacebookUser_ReceiverId",
                table: "ScheduleReceiverMapping",
                column: "ReceiverId",
                principalTable: "FacebookUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleReceiverMapping_Schedule_ScheduleId",
                table: "ScheduleReceiverMapping",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleWeekdayMapping_Schedule_ScheduleId",
                table: "ScheduleWeekdayMapping",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUserPermissionMapping_LoginUser_LoginUserId",
                table: "LoginUserPermissionMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleJob_Schedule_ScheduleId",
                table: "ScheduleJob");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleJobResult_ScheduleJob_ScheduleJobId",
                table: "ScheduleJobResult");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleReceiverMapping_FacebookUser_ReceiverId",
                table: "ScheduleReceiverMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleReceiverMapping_Schedule_ScheduleId",
                table: "ScheduleReceiverMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleWeekdayMapping_Schedule_ScheduleId",
                table: "ScheduleWeekdayMapping");

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Schedule",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUserPermissionMapping_LoginUser_LoginUserId",
                table: "LoginUserPermissionMapping",
                column: "LoginUserId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule",
                column: "OwnerId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleJob_Schedule_ScheduleId",
                table: "ScheduleJob",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleJobResult_ScheduleJob_ScheduleJobId",
                table: "ScheduleJobResult",
                column: "ScheduleJobId",
                principalTable: "ScheduleJob",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleReceiverMapping_FacebookUser_ReceiverId",
                table: "ScheduleReceiverMapping",
                column: "ReceiverId",
                principalTable: "FacebookUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleReceiverMapping_Schedule_ScheduleId",
                table: "ScheduleReceiverMapping",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleWeekdayMapping_Schedule_ScheduleId",
                table: "ScheduleWeekdayMapping",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
