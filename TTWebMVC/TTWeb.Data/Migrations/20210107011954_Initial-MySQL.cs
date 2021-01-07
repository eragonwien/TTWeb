using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace TTWeb.Data.Migrations
{
    public partial class InitialMySQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 128, nullable: false),
                    FirstName = table.Column<string>(maxLength: 128, nullable: false),
                    LastName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacebookUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 128, nullable: false),
                    Password = table.Column<string>(maxLength: 128, nullable: true),
                    HomeAddress = table.Column<string>(maxLength: 128, nullable: true),
                    ProfileAddress = table.Column<string>(maxLength: 128, nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacebookUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacebookUser_LoginUser_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "LoginUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginUserPermissionMapping",
                columns: table => new
                {
                    LoginUserId = table.Column<int>(nullable: false),
                    UserPermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginUserPermissionMapping", x => new { x.LoginUserId, x.UserPermissionId });
                    table.ForeignKey(
                        name: "FK_LoginUserPermissionMapping_LoginUser_LoginUserId",
                        column: x => x.LoginUserId,
                        principalTable: "LoginUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(maxLength: 64, nullable: false),
                    IntervalType = table.Column<string>(maxLength: 64, nullable: false),
                    PlanningStatusId = table.Column<int>(nullable: false, defaultValue: 1),
                    SenderId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    LockedUntil = table.Column<DateTime>(nullable: true),
                    LockAt = table.Column<DateTime>(nullable: true),
                    CompletedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_LoginUser_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "LoginUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule_FacebookUser_SenderId",
                        column: x => x.SenderId,
                        principalTable: "FacebookUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleJob",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ScheduleId = table.Column<int>(nullable: false),
                    Action = table.Column<int>(nullable: false),
                    SenderId = table.Column<int>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    LockAt = table.Column<DateTime>(nullable: true),
                    LockedUntil = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleJob_FacebookUser_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "FacebookUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleJob_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleJob_FacebookUser_SenderId",
                        column: x => x.SenderId,
                        principalTable: "FacebookUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleReceiverMapping",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleReceiverMapping", x => new { x.ScheduleId, x.ReceiverId });
                    table.ForeignKey(
                        name: "FK_ScheduleReceiverMapping_FacebookUser_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "FacebookUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleReceiverMapping_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTimeFrame",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false),
                    From = table.Column<TimeSpan>(nullable: false),
                    To = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTimeFrame", x => new { x.ScheduleId, x.From, x.To });
                    table.ForeignKey(
                        name: "FK_ScheduleTimeFrame_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleWeekdayMapping",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false),
                    Weekday = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleWeekdayMapping", x => new { x.ScheduleId, x.Weekday });
                    table.ForeignKey(
                        name: "FK_ScheduleWeekdayMapping_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleJobResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ScheduleJobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleJobResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleJobResult_ScheduleJob_ScheduleJobId",
                        column: x => x.ScheduleJobId,
                        principalTable: "ScheduleJob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LoginUser",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "test@test.com", "test", "dev" });

            migrationBuilder.InsertData(
                table: "FacebookUser",
                columns: new[] { "Id", "Enabled", "HomeAddress", "OwnerId", "Password", "ProfileAddress", "Username" },
                values: new object[] { 1, true, null, 1, "1234", null, "eragonwien@gmail.com" });

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
                columns: new[] { "Id", "ScheduleJobId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_FacebookUser_OwnerId",
                table: "FacebookUser",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FacebookUser_Username",
                table: "FacebookUser",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginUser_Email",
                table: "LoginUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_OwnerId",
                table: "Schedule",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_SenderId",
                table: "Schedule",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJob_ReceiverId",
                table: "ScheduleJob",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJob_SenderId",
                table: "ScheduleJob",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJob_ScheduleId_StartDate_Action_ReceiverId_SenderId",
                table: "ScheduleJob",
                columns: new[] { "ScheduleId", "StartDate", "Action", "ReceiverId", "SenderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJobResult_ScheduleJobId",
                table: "ScheduleJobResult",
                column: "ScheduleJobId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReceiverMapping_ReceiverId",
                table: "ScheduleReceiverMapping",
                column: "ReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginUserPermissionMapping");

            migrationBuilder.DropTable(
                name: "ScheduleJobResult");

            migrationBuilder.DropTable(
                name: "ScheduleReceiverMapping");

            migrationBuilder.DropTable(
                name: "ScheduleTimeFrame");

            migrationBuilder.DropTable(
                name: "ScheduleWeekdayMapping");

            migrationBuilder.DropTable(
                name: "ScheduleJob");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "FacebookUser");

            migrationBuilder.DropTable(
                name: "LoginUser");
        }
    }
}
