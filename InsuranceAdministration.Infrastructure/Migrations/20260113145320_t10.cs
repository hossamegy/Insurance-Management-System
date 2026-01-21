using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcquaintanceDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SoldierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcquaintanceDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcquaintanceDocument_Soldier_SoldierId",
                        column: x => x.SoldierId,
                        principalTable: "Soldier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoldierLeave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartNum = table.Column<int>(type: "INTEGER", nullable: true),
                    StartPage = table.Column<int>(type: "INTEGER", nullable: true),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndNum = table.Column<int>(type: "INTEGER", nullable: true),
                    EndPage = table.Column<int>(type: "INTEGER", nullable: true),
                    End = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SoldierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldierLeave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoldierLeave_Soldier_SoldierId",
                        column: x => x.SoldierId,
                        principalTable: "Soldier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseFamily",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Relationship = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NationalId = table.Column<string>(type: "TEXT", nullable: true),
                    Job = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    AcquaintanceDocumentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseFamily", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseFamily_AcquaintanceDocument_AcquaintanceDocumentId",
                        column: x => x.AcquaintanceDocumentId,
                        principalTable: "AcquaintanceDocument",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Relationship = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NationalId = table.Column<string>(type: "TEXT", nullable: true),
                    Job = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    AcquaintanceDocumentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Family_AcquaintanceDocument_AcquaintanceDocumentId",
                        column: x => x.AcquaintanceDocumentId,
                        principalTable: "AcquaintanceDocument",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcquaintanceDocument_SoldierId",
                table: "AcquaintanceDocument",
                column: "SoldierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseFamily_AcquaintanceDocumentId",
                table: "BaseFamily",
                column: "AcquaintanceDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Family_AcquaintanceDocumentId",
                table: "Family",
                column: "AcquaintanceDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SoldierLeave_SoldierId",
                table: "SoldierLeave",
                column: "SoldierId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseFamily");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "SoldierLeave");

            migrationBuilder.DropTable(
                name: "AcquaintanceDocument");
        }
    }
}
