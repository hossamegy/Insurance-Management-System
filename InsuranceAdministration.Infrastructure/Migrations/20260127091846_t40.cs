using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mission_DailyMissions_DailyMissionId",
                table: "Mission");

            migrationBuilder.DropIndex(
                name: "IX_Mission_DailyMissionId",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "DailyMissionId",
                table: "Mission");

            migrationBuilder.AddColumn<int>(
                name: "DailyMissionId",
                table: "SoldierMission",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoldierMission_DailyMissionId",
                table: "SoldierMission",
                column: "DailyMissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldierMission_DailyMissions_DailyMissionId",
                table: "SoldierMission",
                column: "DailyMissionId",
                principalTable: "DailyMissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldierMission_DailyMissions_DailyMissionId",
                table: "SoldierMission");

            migrationBuilder.DropIndex(
                name: "IX_SoldierMission_DailyMissionId",
                table: "SoldierMission");

            migrationBuilder.DropColumn(
                name: "DailyMissionId",
                table: "SoldierMission");

            migrationBuilder.AddColumn<int>(
                name: "DailyMissionId",
                table: "Mission",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mission_DailyMissionId",
                table: "Mission",
                column: "DailyMissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_DailyMissions_DailyMissionId",
                table: "Mission",
                column: "DailyMissionId",
                principalTable: "DailyMissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
