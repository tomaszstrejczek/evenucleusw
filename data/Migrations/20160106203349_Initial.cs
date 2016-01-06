using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace ts.data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchiveSession",
                columns: table => new
                {
                    ArchiveSessionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SessionAccess = table.Column<DateTime>(nullable: false),
                    SessionEnd = table.Column<DateTime>(nullable: false),
                    SessionId = table.Column<string>(nullable: true),
                    SessionStart = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveSession", x => x.ArchiveSessionId);
                });
            migrationBuilder.CreateTable(
                name: "CacheEntry",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    CachedUntil = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheEntry", x => x.Key);
                });
            migrationBuilder.CreateTable(
                name: "TypeNameEntry",
                columns: table => new
                {
                    Key = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CachedUntil = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeNameEntry", x => x.Key);
                });
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    HashedPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });
            migrationBuilder.CreateTable(
                name: "Corporation",
                columns: table => new
                {
                    CorporationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EveId = table.Column<long>(nullable: false),
                    KeyInfoId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    UserUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation", x => x.CorporationId);
                    table.ForeignKey(
                        name: "FK_Corporation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corporation_User_UserUserId",
                        column: x => x.UserUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsManufacturing = table.Column<bool>(nullable: false),
                    JobCompleted = table.Column<bool>(nullable: false),
                    JobDescription = table.Column<string>(nullable: true),
                    JobEnd = table.Column<DateTime>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    PercentageOfCompletion = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    UserUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Job_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_User_UserUserId",
                        column: x => x.UserUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "KeyInfo",
                columns: table => new
                {
                    KeyInfoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KeyId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    UserUserId = table.Column<long>(nullable: true),
                    VCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyInfo", x => x.KeyInfoId);
                    table.ForeignKey(
                        name: "FK_KeyInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeyInfo_User_UserUserId",
                        column: x => x.UserUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Error = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Message2 = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    UserUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserUserId",
                        column: x => x.UserUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Pilot",
                columns: table => new
                {
                    PilotId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentTrainingEnd = table.Column<DateTime>(nullable: false),
                    CurrentTrainingNameAndLevel = table.Column<string>(nullable: true),
                    EveId = table.Column<long>(nullable: false),
                    FreeManufacturingJobsNofificationCount = table.Column<int>(nullable: false),
                    FreeResearchJobsNofificationCount = table.Column<int>(nullable: false),
                    KeyInfoId = table.Column<long>(nullable: false),
                    MaxManufacturingJobs = table.Column<int>(nullable: false),
                    MaxResearchJobs = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TrainingActive = table.Column<bool>(nullable: false),
                    TrainingQueueEnd = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    UserUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilot", x => x.PilotId);
                    table.ForeignKey(
                        name: "FK_Pilot_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pilot_User_UserUserId",
                        column: x => x.UserUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    SessionId = table.Column<string>(nullable: false),
                    SessionAccess = table.Column<DateTime>(nullable: false),
                    SessionEnd = table.Column<DateTime>(nullable: false),
                    SessionStart = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Session_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(nullable: false),
                    PilotId = table.Column<long>(nullable: false),
                    PilotPilotId = table.Column<long>(nullable: true),
                    PilotPilotId1 = table.Column<long>(nullable: true),
                    SkillName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_Skill_Pilot_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Pilot",
                        principalColumn: "PilotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skill_Pilot_PilotPilotId",
                        column: x => x.PilotPilotId,
                        principalTable: "Pilot",
                        principalColumn: "PilotId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Skill_Pilot_PilotPilotId1",
                        column: x => x.PilotPilotId1,
                        principalTable: "Pilot",
                        principalColumn: "PilotId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "SkillInQueue",
                columns: table => new
                {
                    SkillInQueueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Length = table.Column<TimeSpan>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PilotId = table.Column<long>(nullable: false),
                    PilotPilotId = table.Column<long>(nullable: true),
                    PilotPilotId1 = table.Column<long>(nullable: true),
                    SkillName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillInQueue", x => x.SkillInQueueId);
                    table.ForeignKey(
                        name: "FK_SkillInQueue_Pilot_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Pilot",
                        principalColumn: "PilotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillInQueue_Pilot_PilotPilotId",
                        column: x => x.PilotPilotId,
                        principalTable: "Pilot",
                        principalColumn: "PilotId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkillInQueue_Pilot_PilotPilotId1",
                        column: x => x.PilotPilotId1,
                        principalTable: "Pilot",
                        principalColumn: "PilotId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("ArchiveSession");
            migrationBuilder.DropTable("CacheEntry");
            migrationBuilder.DropTable("Corporation");
            migrationBuilder.DropTable("Job");
            migrationBuilder.DropTable("KeyInfo");
            migrationBuilder.DropTable("Notification");
            migrationBuilder.DropTable("Session");
            migrationBuilder.DropTable("Skill");
            migrationBuilder.DropTable("SkillInQueue");
            migrationBuilder.DropTable("TypeNameEntry");
            migrationBuilder.DropTable("Pilot");
            migrationBuilder.DropTable("User");
        }
    }
}
