using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mission_DailyMission_DailyMissionId",
                table: "Mission");

            migrationBuilder.DropForeignKey(
                name: "FK_Policemen_Mission_MissionId",
                table: "Policemen");

            migrationBuilder.DropForeignKey(
                name: "FK_Soldier_Mission_MissionId",
                table: "Soldier");

            migrationBuilder.DropIndex(
                name: "IX_Soldier_MissionId",
                table: "Soldier");

            migrationBuilder.DropIndex(
                name: "IX_Policemen_MissionId",
                table: "Policemen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyMission",
                table: "DailyMission");

            migrationBuilder.DropColumn(
                name: "MissionId",
                table: "Soldier");

            migrationBuilder.DropColumn(
                name: "MissionId",
                table: "Policemen");

            migrationBuilder.RenameTable(
                name: "DailyMission",
                newName: "DailyMissions");

            migrationBuilder.AddColumn<string>(
                name: "BoatNumber",
                table: "Mission",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WirelessCallSign",
                table: "Mission",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyMissions",
                table: "DailyMissions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MissionPoliceMan",
                columns: table => new
                {
                    MissionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    PolicemenId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionPoliceMan", x => new { x.MissionsId, x.PolicemenId });
                    table.ForeignKey(
                        name: "FK_MissionPoliceMan_Mission_MissionsId",
                        column: x => x.MissionsId,
                        principalTable: "Mission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionPoliceMan_Policemen_PolicemenId",
                        column: x => x.PolicemenId,
                        principalTable: "Policemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MissionSoldier",
                columns: table => new
                {
                    MissionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SoldiersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionSoldier", x => new { x.MissionsId, x.SoldiersId });
                    table.ForeignKey(
                        name: "FK_MissionSoldier_Mission_MissionsId",
                        column: x => x.MissionsId,
                        principalTable: "Mission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionSoldier_Soldier_SoldiersId",
                        column: x => x.SoldiersId,
                        principalTable: "Soldier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionPoliceMan_PolicemenId",
                table: "MissionPoliceMan",
                column: "PolicemenId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionSoldier_SoldiersId",
                table: "MissionSoldier",
                column: "SoldiersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_DailyMissions_DailyMissionId",
                table: "Mission",
                column: "DailyMissionId",
                principalTable: "DailyMissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mission_DailyMissions_DailyMissionId",
                table: "Mission");

            migrationBuilder.DropTable(
                name: "MissionPoliceMan");

            migrationBuilder.DropTable(
                name: "MissionSoldier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyMissions",
                table: "DailyMissions");

            migrationBuilder.DropColumn(
                name: "BoatNumber",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "WirelessCallSign",
                table: "Mission");

            migrationBuilder.RenameTable(
                name: "DailyMissions",
                newName: "DailyMission");

            migrationBuilder.AddColumn<int>(
                name: "MissionId",
                table: "Soldier",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MissionId",
                table: "Policemen",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyMission",
                table: "DailyMission",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Soldier_MissionId",
                table: "Soldier",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Policemen_MissionId",
                table: "Policemen",
                column: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_DailyMission_DailyMissionId",
                table: "Mission",
                column: "DailyMissionId",
                principalTable: "DailyMission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policemen_Mission_MissionId",
                table: "Policemen",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Soldier_Mission_MissionId",
                table: "Soldier",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "Id");
        }
    }
}
