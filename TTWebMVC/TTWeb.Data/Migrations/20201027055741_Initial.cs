using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTWeb.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacebookUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 64, nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: false),
                    SendScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacebookUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 128, nullable: false),
                    FirstName = table.Column<string>(maxLength: 128, nullable: false),
                    LastName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(maxLength: 64, nullable: false),
                    IntervalType = table.Column<string>(maxLength: 64, nullable: false),
                    SenderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_FacebookUser_SenderId",
                        column: x => x.SenderId,
                        principalTable: "FacebookUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginUserPermissionMappings",
                columns: table => new
                {
                    LoginUserId = table.Column<int>(nullable: false),
                    UserPermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginUserPermissionMappings", x => new { x.LoginUserId, x.UserPermissionId });
                    table.ForeignKey(
                        name: "FK_LoginUserPermissionMappings_LoginUsers_LoginUserId",
                        column: x => x.LoginUserId,
                        principalTable: "LoginUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleJob",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleJob_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleReceiverMapping_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeFrame",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScheduleId = table.Column<int>(nullable: false),
                    From = table.Column<DateTimeOffset>(nullable: false),
                    To = table.Column<DateTimeOffset>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ScheduleJobResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacebookUser_Username",
                table: "FacebookUser",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginUsers_Email",
                table: "LoginUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_SenderId",
                table: "Schedule",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJob_ScheduleId",
                table: "ScheduleJob",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJobResult_ScheduleJobId",
                table: "ScheduleJobResult",
                column: "ScheduleJobId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReceiverMapping_ReceiverId",
                table: "ScheduleReceiverMapping",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeFrame_ScheduleId",
                table: "TimeFrame",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginUserPermissionMappings");

            migrationBuilder.DropTable(
                name: "ScheduleJobResult");

            migrationBuilder.DropTable(
                name: "ScheduleReceiverMapping");

            migrationBuilder.DropTable(
                name: "ScheduleWeekdayMapping");

            migrationBuilder.DropTable(
                name: "TimeFrame");

            migrationBuilder.DropTable(
                name: "LoginUsers");

            migrationBuilder.DropTable(
                name: "ScheduleJob");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "FacebookUser");
        }
    }
}
