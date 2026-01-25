using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoliticalAndCriminal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PoliticalStatus = table.Column<string>(type: "TEXT", nullable: true),
                    CriminalRecord = table.Column<string>(type: "TEXT", nullable: true),
                    SoldierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticalAndCriminal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliticalAndCriminal_Soldier_SoldierId",
                        column: x => x.SoldierId,
                        principalTable: "Soldier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TraingName = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Institution = table.Column<string>(type: "TEXT", nullable: true),
                    SoldierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_Soldier_SoldierId",
                        column: x => x.SoldierId,
                        principalTable: "Soldier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalAndCriminal_SoldierId",
                table: "PoliticalAndCriminal",
                column: "SoldierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Training_SoldierId",
                table: "Training",
                column: "SoldierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoliticalAndCriminal");

            migrationBuilder.DropTable(
                name: "Training");
        }
    }
}
