using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class ChangesDeleteBehaviourToCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacebookUser_LoginUser_OwnerId",
                table: "FacebookUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_FacebookUser_SenderId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleJob_FacebookUser_ReceiverId",
                table: "ScheduleJob");

            migrationBuilder.DeleteData(
                table: "LoginUserPermissionMapping",
                keyColumns: new[] { "LoginUserId", "UserPermissionId" },
                keyValues: new object[] { 1, 1 });

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

            migrationBuilder.DeleteData(
                table: "LoginUserPermissionMapping",
                keyColumns: new[] { "LoginUserId", "UserPermissionId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "ScheduleJobResult",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ScheduleReceiverMapping",
                keyColumns: new[] { "ScheduleId", "ReceiverId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ScheduleTimeFrame",
                keyColumns: new[] { "ScheduleId", "From", "To" },
                keyValues: new object[] { 1, new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0) });

            migrationBuilder.DeleteData(
                table: "ScheduleWeekdayMapping",
                keyColumns: new[] { "ScheduleId", "Weekday" },
                keyValues: new object[] { 1, "Friday" });

            migrationBuilder.DeleteData(
                table: "ScheduleWeekdayMapping",
                keyColumns: new[] { "ScheduleId", "Weekday" },
                keyValues: new object[] { 1, "Monday" });

            migrationBuilder.DeleteData(
                table: "ScheduleWeekdayMapping",
                keyColumns: new[] { "ScheduleId", "Weekday" },
                keyValues: new object[] { 1, "Wednesday" });

            migrationBuilder.DeleteData(
                table: "ScheduleJob",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FacebookUser",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LoginUser",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_FacebookUser_LoginUser_OwnerId",
                table: "FacebookUser",
                column: "OwnerId",
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
                name: "FK_Schedule_FacebookUser_SenderId",
                table: "Schedule",
                column: "SenderId",
                principalTable: "FacebookUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleJob_FacebookUser_ReceiverId",
                table: "ScheduleJob",
                column: "ReceiverId",
                principalTable: "FacebookUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacebookUser_LoginUser_OwnerId",
                table: "FacebookUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_LoginUser_OwnerId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_FacebookUser_SenderId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleJob_FacebookUser_ReceiverId",
                table: "ScheduleJob");

            migrationBuilder.InsertData(
                table: "LoginUser",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "test@test.com", "test", "dev" });

            migrationBuilder.InsertData(
                table: "FacebookUser",
                columns: new[] { "Id", "Enabled", "OwnerId", "Password", "SeedCode", "UserCode", "Username" },
                values: new object[] { 1, true, 1, "1234", null, null, "eragonwien@gmail.com" });

            migrationBuilder.InsertData(
                table: "LoginUserPermissionMapping",
                columns: new[] { "LoginUserId", "UserPermissionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 6 }
                });

            migrationBuilder.InsertData(
                table: "Schedule",
                columns: new[] { "Id", "Action", "CompletedAt", "IntervalType", "LockAt", "LockedUntil", "OwnerId", "SenderId" },
                values: new object[] { 1, "Like", null, "Daily", null, null, 1, 1 });

            migrationBuilder.InsertData(
                table: "ScheduleJob",
                columns: new[] { "Id", "Action", "EndTime", "LockAt", "LockedUntil", "ReceiverId", "ScheduleId", "SenderId", "StartDate", "StartTime", "Status" },
                values: new object[] { 1, 1, null, null, null, 1, 1, 1, null, null, 5 });

            migrationBuilder.InsertData(
                table: "ScheduleReceiverMapping",
                columns: new[] { "ScheduleId", "ReceiverId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ScheduleTimeFrame",
                columns: new[] { "ScheduleId", "From", "To" },
                values: new object[] { 1, new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "ScheduleWeekdayMapping",
                columns: new[] { "ScheduleId", "Weekday" },
                values: new object[,]
                {
                    { 1, "Monday" },
                    { 1, "Wednesday" },
                    { 1, "Friday" }
                });

            migrationBuilder.InsertData(
                table: "ScheduleJobResult",
                columns: new[] { "Id", "ErrorMessage", "Message", "ScheduleJobId", "Status" },
                values: new object[] { 1, null, null, 1, 0 });

            migrationBuilder.AddForeignKey(
                name: "FK_FacebookUser_LoginUser_OwnerId",
                table: "FacebookUser",
                column: "OwnerId",
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
                name: "FK_Schedule_FacebookUser_SenderId",
                table: "Schedule",
                column: "SenderId",
                principalTable: "FacebookUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleJob_FacebookUser_ReceiverId",
                table: "ScheduleJob",
                column: "ReceiverId",
                principalTable: "FacebookUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
