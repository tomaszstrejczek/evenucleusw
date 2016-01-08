using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ts.data.Migrations
{
    public partial class Fix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Corporation_User_UserId", table: "Corporation");
            migrationBuilder.DropForeignKey(name: "FK_Job_User_UserId", table: "Job");
            migrationBuilder.DropForeignKey(name: "FK_KeyInfo_User_UserId", table: "KeyInfo");
            migrationBuilder.DropForeignKey(name: "FK_Notification_User_UserId", table: "Notification");
            migrationBuilder.DropForeignKey(name: "FK_Pilot_User_UserId", table: "Pilot");
            migrationBuilder.DropForeignKey(name: "FK_Session_User_UserId", table: "Session");
            migrationBuilder.DropForeignKey(name: "FK_Skill_Pilot_PilotId", table: "Skill");
            migrationBuilder.DropForeignKey(name: "FK_SkillInQueue_Pilot_PilotId", table: "SkillInQueue");
            migrationBuilder.DropColumn(name: "Length", table: "SkillInQueue");
            migrationBuilder.AddColumn<long>(
                name: "LengthTicks",
                table: "SkillInQueue",
                nullable: false,
                defaultValue: 0L);
            migrationBuilder.AddForeignKey(
                name: "FK_Corporation_User_UserId",
                table: "Corporation",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Job_User_UserId",
                table: "Job",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_KeyInfo_User_UserId",
                table: "KeyInfo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Pilot_User_UserId",
                table: "Pilot",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Session_User_UserId",
                table: "Session",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Pilot_PilotId",
                table: "Skill",
                column: "PilotId",
                principalTable: "Pilot",
                principalColumn: "PilotId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_SkillInQueue_Pilot_PilotId",
                table: "SkillInQueue",
                column: "PilotId",
                principalTable: "Pilot",
                principalColumn: "PilotId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Corporation_User_UserId", table: "Corporation");
            migrationBuilder.DropForeignKey(name: "FK_Job_User_UserId", table: "Job");
            migrationBuilder.DropForeignKey(name: "FK_KeyInfo_User_UserId", table: "KeyInfo");
            migrationBuilder.DropForeignKey(name: "FK_Notification_User_UserId", table: "Notification");
            migrationBuilder.DropForeignKey(name: "FK_Pilot_User_UserId", table: "Pilot");
            migrationBuilder.DropForeignKey(name: "FK_Session_User_UserId", table: "Session");
            migrationBuilder.DropForeignKey(name: "FK_Skill_Pilot_PilotId", table: "Skill");
            migrationBuilder.DropForeignKey(name: "FK_SkillInQueue_Pilot_PilotId", table: "SkillInQueue");
            migrationBuilder.DropColumn(name: "LengthTicks", table: "SkillInQueue");
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Length",
                table: "SkillInQueue",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
            migrationBuilder.AddForeignKey(
                name: "FK_Corporation_User_UserId",
                table: "Corporation",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Job_User_UserId",
                table: "Job",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_KeyInfo_User_UserId",
                table: "KeyInfo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Pilot_User_UserId",
                table: "Pilot",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Session_User_UserId",
                table: "Session",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Pilot_PilotId",
                table: "Skill",
                column: "PilotId",
                principalTable: "Pilot",
                principalColumn: "PilotId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_SkillInQueue_Pilot_PilotId",
                table: "SkillInQueue",
                column: "PilotId",
                principalTable: "Pilot",
                principalColumn: "PilotId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
