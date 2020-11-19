using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class ChangesScheduleWorkerReferenceFromLoginUserToWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_LoginUser_WorkerId",
                table: "Schedule");

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Secret = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerPermissionMapping",
                columns: table => new
                {
                    WorkerId = table.Column<int>(nullable: false),
                    UserPermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerPermissionMapping", x => new { x.UserPermissionId, x.WorkerId });
                    table.ForeignKey(
                        name: "FK_WorkerPermissionMapping_Worker_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Worker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ScheduleJob",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2020, 11, 19, 0, 40, 36, 892, DateTimeKind.Utc).AddTicks(284));

            migrationBuilder.InsertData(
                table: "Worker",
                columns: new[] { "Id", "Secret" },
                values: new object[] { 1, "113099c9-2822-4e80-961f-fce98ccfeaef" });

            migrationBuilder.InsertData(
                table: "WorkerPermissionMapping",
                columns: new[] { "UserPermissionId", "WorkerId" },
                values: new object[] { 5, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_WorkerPermissionMapping_WorkerId",
                table: "WorkerPermissionMapping",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Worker_WorkerId",
                table: "Schedule",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Worker_WorkerId",
                table: "Schedule");

            migrationBuilder.DropTable(
                name: "WorkerPermissionMapping");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.UpdateData(
                table: "Schedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanningStatusId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ScheduleJob",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2020, 11, 19, 0, 27, 17, 676, DateTimeKind.Utc).AddTicks(3110));

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_LoginUser_WorkerId",
                table: "Schedule",
                column: "WorkerId",
                principalTable: "LoginUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
