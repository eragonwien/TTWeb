using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeFrame");

            migrationBuilder.DropColumn(
                name: "SendScheduleId",
                table: "FacebookUser");

            migrationBuilder.CreateTable(
                name: "ScheduleTimeFrame",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScheduleId = table.Column<int>(nullable: false),
                    From = table.Column<TimeSpan>(nullable: false),
                    To = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTimeFrame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleTimeFrame_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FacebookUser",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 1, "1234", "eragonwien@gmail.com" });

            migrationBuilder.InsertData(
                table: "LoginUser",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "test@test.com", "test", "dev" });

            migrationBuilder.InsertData(
                table: "LoginUserPermissionMapping",
                columns: new[] { "LoginUserId", "UserPermissionId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Schedule",
                columns: new[] { "Id", "Action", "IntervalType", "SenderId" },
                values: new object[] { 1, "LIKE", "Daily", 1 });

            migrationBuilder.InsertData(
                table: "ScheduleJob",
                columns: new[] { "Id", "ScheduleId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ScheduleReceiverMapping",
                columns: new[] { "ScheduleId", "ReceiverId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ScheduleTimeFrame",
                columns: new[] { "Id", "From", "ScheduleId", "To" },
                values: new object[] { 1, new TimeSpan(0, 9, 0, 0, 0), 1, new TimeSpan(0, 14, 0, 0, 0) });

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
                name: "IX_ScheduleTimeFrame_ScheduleId",
                table: "ScheduleTimeFrame",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleTimeFrame");

            migrationBuilder.DeleteData(
                table: "LoginUserPermissionMapping",
                keyColumns: new[] { "LoginUserId", "UserPermissionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ScheduleJobResult",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ScheduleReceiverMapping",
                keyColumns: new[] { "ScheduleId", "ReceiverId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ScheduleWeekdayMapping",
                keyColumns: new[] { "ScheduleId", "Weekday" },
                keyValues: new object[] { 1, "Monday" });

            migrationBuilder.DeleteData(
                table: "ScheduleWeekdayMapping",
                keyColumns: new[] { "ScheduleId", "Weekday" },
                keyValues: new object[] { 1, "Wednesday" });

            migrationBuilder.DeleteData(
                table: "ScheduleWeekdayMapping",
                keyColumns: new[] { "ScheduleId", "Weekday" },
                keyValues: new object[] { 1, "Friday" });

            migrationBuilder.DeleteData(
                table: "LoginUser",
                keyColumn: "Id",
                keyValue: 1);

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

            migrationBuilder.AddColumn<int>(
                name: "SendScheduleId",
                table: "FacebookUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TimeFrame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    From = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    To = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeFrame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeFrame_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeFrame_ScheduleId",
                table: "TimeFrame",
                column: "ScheduleId");
        }
    }
}
