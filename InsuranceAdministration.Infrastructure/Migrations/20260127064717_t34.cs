using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionSoldier");

            migrationBuilder.CreateTable(
                name: "SoldierMission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SoldierId = table.Column<int>(type: "INTEGER", nullable: false),
                    MissionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldierMission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoldierMission_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoldierMission_Soldier_SoldierId",
                        column: x => x.SoldierId,
                        principalTable: "Soldier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoldierMission_MissionId",
                table: "SoldierMission",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SoldierMission_SoldierId_MissionId",
                table: "SoldierMission",
                columns: new[] { "SoldierId", "MissionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoldierMission");

            migrationBuilder.CreateTable(
                name: "MissionSoldier",
                columns: table => new
                {
                    MissionId = table.Column<int>(type: "INTEGER", nullable: false),
                    SoldierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionSoldier", x => new { x.MissionId, x.SoldierId });
                    table.ForeignKey(
                        name: "FK_MissionSoldier_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionSoldier_Soldier_SoldierId",
                        column: x => x.SoldierId,
                        principalTable: "Soldier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionSoldier_SoldierId",
                table: "MissionSoldier",
                column: "SoldierId");
        }
    }
}
