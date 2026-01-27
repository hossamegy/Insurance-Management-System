using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t37 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MissionId1",
                table: "SoldierMission",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoldierMission_AssignedAt",
                table: "SoldierMission",
                column: "AssignedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SoldierMission_MissionId1",
                table: "SoldierMission",
                column: "MissionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldierMission_Mission_MissionId1",
                table: "SoldierMission",
                column: "MissionId1",
                principalTable: "Mission",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldierMission_Mission_MissionId1",
                table: "SoldierMission");

            migrationBuilder.DropIndex(
                name: "IX_SoldierMission_AssignedAt",
                table: "SoldierMission");

            migrationBuilder.DropIndex(
                name: "IX_SoldierMission_MissionId1",
                table: "SoldierMission");

            migrationBuilder.DropColumn(
                name: "MissionId1",
                table: "SoldierMission");
        }
    }
}
