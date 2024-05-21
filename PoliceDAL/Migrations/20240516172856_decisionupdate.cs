using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class decisionupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "decision",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JudgeId = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_decision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_decision_AspNetUsers_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "warrant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    suspect_id = table.Column<int>(type: "int", nullable: false),
                    case_file_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warrant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_warrant_AspNetUsers_suspect_id",
                        column: x => x.suspect_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_warrant_case_file_case_file_id",
                        column: x => x.case_file_id,
                        principalTable: "case_file",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_warrant_decision_Id",
                        column: x => x.Id,
                        principalTable: "decision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_decision_JudgeId",
                table: "decision",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_warrant_case_file_id",
                table: "warrant",
                column: "case_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_warrant_suspect_id",
                table: "warrant",
                column: "suspect_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "warrant");

            migrationBuilder.DropTable(
                name: "decision");
        }
    }
}
