using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t38 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SoldierMission_SoldierId_MissionId",
                table: "SoldierMission");

            migrationBuilder.CreateIndex(
                name: "IX_SoldierMission_SoldierId_MissionId",
                table: "SoldierMission",
                columns: new[] { "SoldierId", "MissionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SoldierMission_SoldierId_MissionId",
                table: "SoldierMission");

            migrationBuilder.CreateIndex(
                name: "IX_SoldierMission_SoldierId_MissionId",
                table: "SoldierMission",
                columns: new[] { "SoldierId", "MissionId" },
                unique: true);
        }
    }
}
