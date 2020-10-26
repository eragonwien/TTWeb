using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class AddEnumCoversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUserPermissionMappings_LoginUsers_LoginUserId",
                table: "LoginUserPermissionMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginUserPermissionMappings_UserPermissions_UserPermissionId",
                table: "LoginUserPermissionMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_FacebookUser_SenderId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleWeekdayMapping_Weekday_WeekdayId",
                table: "ScheduleWeekdayMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeFrame_Schedule_ScheduleId",
                table: "TimeFrame");

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "UserPermissions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUserPermissionMappings_LoginUsers_LoginUserId",
                table: "LoginUserPermissionMappings",
                column: "LoginUserId",
                principalTable: "LoginUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUserPermissionMappings_UserPermissions_UserPermissionId",
                table: "LoginUserPermissionMappings",
                column: "UserPermissionId",
                principalTable: "UserPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_FacebookUser_SenderId",
                table: "Schedule",
                column: "SenderId",
                principalTable: "FacebookUser",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleWeekdayMapping_Weekday_WeekdayId",
                table: "ScheduleWeekdayMapping",
                column: "WeekdayId",
                principalTable: "Weekday",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeFrame_Schedule_ScheduleId",
                table: "TimeFrame",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUserPermissionMappings_LoginUsers_LoginUserId",
                table: "LoginUserPermissionMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginUserPermissionMappings_UserPermissions_UserPermissionId",
                table: "LoginUserPermissionMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_FacebookUser_SenderId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleWeekdayMapping_Weekday_WeekdayId",
                table: "ScheduleWeekdayMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeFrame_Schedule_ScheduleId",
                table: "TimeFrame");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "UserPermissions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUserPermissionMappings_LoginUsers_LoginUserId",
                table: "LoginUserPermissionMappings",
                column: "LoginUserId",
                principalTable: "LoginUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUserPermissionMappings_UserPermissions_UserPermissionId",
                table: "LoginUserPermissionMappings",
                column: "UserPermissionId",
                principalTable: "UserPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_FacebookUser_SenderId",
                table: "Schedule",
                column: "SenderId",
                principalTable: "FacebookUser",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleWeekdayMapping_Weekday_WeekdayId",
                table: "ScheduleWeekdayMapping",
                column: "WeekdayId",
                principalTable: "Weekday",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeFrame_Schedule_ScheduleId",
                table: "TimeFrame",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
