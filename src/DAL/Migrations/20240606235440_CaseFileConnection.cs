using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class CaseFileConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prosecutor_case_file",
                table: "case_file");

            migrationBuilder.DropIndex(
                name: "IX_case_file_prosecutor_id",
                table: "case_file");

            migrationBuilder.DropColumn(
                name: "prosecutor_id",
                table: "case_file");

            migrationBuilder.AlterColumn<int>(
                name: "case_file_id",
                table: "report",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "case_file_assignation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseFileId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_file_assignation", x => x.id);
                    table.ForeignKey(
                        name: "FK_case_file_assignation_AspNetUsers_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_file_assignation_case_file_CaseFileId",
                        column: x => x.CaseFileId,
                        principalTable: "case_file",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_case_file_assignation_CaseFileId",
                table: "case_file_assignation",
                column: "CaseFileId");

            migrationBuilder.CreateIndex(
                name: "IX_case_file_assignation_PersonId",
                table: "case_file_assignation",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "case_file_assignation");

            migrationBuilder.AlterColumn<int>(
                name: "case_file_id",
                table: "report",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "prosecutor_id",
                table: "case_file",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_case_file_prosecutor_id",
                table: "case_file",
                column: "prosecutor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prosecutor_case_file",
                table: "case_file",
                column: "prosecutor_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");
        }
    }
}
