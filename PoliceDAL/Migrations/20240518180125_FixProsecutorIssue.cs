using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixProsecutorIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_decision_AspNetUsers_JudgeId",
                table: "decision");

            migrationBuilder.DropForeignKey(
                name: "FK_warrant_decision_Id",
                table: "warrant");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "warrant",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "decision",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "JudgeId",
                table: "decision",
                newName: "judge_id");

            migrationBuilder.RenameIndex(
                name: "IX_decision_JudgeId",
                table: "decision",
                newName: "IX_decision_judge_id");

            migrationBuilder.AddColumn<DateOnly>(
                name: "issue_date",
                table: "decision",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<int>(
                name: "prosecutor_id",
                table: "case_file",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_decision_AspNetUsers_judge_id",
                table: "decision",
                column: "judge_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_warrant_decision_id",
                table: "warrant",
                column: "id",
                principalTable: "decision",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_decision_AspNetUsers_judge_id",
                table: "decision");

            migrationBuilder.DropForeignKey(
                name: "FK_warrant_decision_id",
                table: "warrant");

            migrationBuilder.DropColumn(
                name: "issue_date",
                table: "decision");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "warrant",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "decision",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "judge_id",
                table: "decision",
                newName: "JudgeId");

            migrationBuilder.RenameIndex(
                name: "IX_decision_judge_id",
                table: "decision",
                newName: "IX_decision_JudgeId");

            migrationBuilder.AlterColumn<int>(
                name: "prosecutor_id",
                table: "case_file",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_decision_AspNetUsers_JudgeId",
                table: "decision",
                column: "JudgeId",
                principalTable: "AspNetUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_warrant_decision_Id",
                table: "warrant",
                column: "Id",
                principalTable: "decision",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
