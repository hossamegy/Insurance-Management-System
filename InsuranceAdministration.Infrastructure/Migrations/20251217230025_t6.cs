using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionPoliceMan_Mission_MissionsId",
                table: "MissionPoliceMan");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionPoliceMan_Policemen_PolicemenId",
                table: "MissionPoliceMan");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionSoldier_Mission_MissionsId",
                table: "MissionSoldier");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionSoldier_Soldier_SoldiersId",
                table: "MissionSoldier");

            migrationBuilder.RenameColumn(
                name: "SoldiersId",
                table: "MissionSoldier",
                newName: "SoldierId");

            migrationBuilder.RenameColumn(
                name: "MissionsId",
                table: "MissionSoldier",
                newName: "MissionId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionSoldier_SoldiersId",
                table: "MissionSoldier",
                newName: "IX_MissionSoldier_SoldierId");

            migrationBuilder.RenameColumn(
                name: "PolicemenId",
                table: "MissionPoliceMan",
                newName: "PoliceManId");

            migrationBuilder.RenameColumn(
                name: "MissionsId",
                table: "MissionPoliceMan",
                newName: "MissionId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionPoliceMan_PolicemenId",
                table: "MissionPoliceMan",
                newName: "IX_MissionPoliceMan_PoliceManId");

            migrationBuilder.CreateIndex(
                name: "IX_Soldier_NationalId",
                table: "Soldier",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Soldier_TripleNumber",
                table: "Soldier",
                column: "TripleNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Policemen_Name",
                table: "Policemen",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionPoliceMan_Mission_MissionId",
                table: "MissionPoliceMan",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionPoliceMan_Policemen_PoliceManId",
                table: "MissionPoliceMan",
                column: "PoliceManId",
                principalTable: "Policemen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionSoldier_Mission_MissionId",
                table: "MissionSoldier",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionSoldier_Soldier_SoldierId",
                table: "MissionSoldier",
                column: "SoldierId",
                principalTable: "Soldier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionPoliceMan_Mission_MissionId",
                table: "MissionPoliceMan");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionPoliceMan_Policemen_PoliceManId",
                table: "MissionPoliceMan");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionSoldier_Mission_MissionId",
                table: "MissionSoldier");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionSoldier_Soldier_SoldierId",
                table: "MissionSoldier");

            migrationBuilder.DropIndex(
                name: "IX_Soldier_NationalId",
                table: "Soldier");

            migrationBuilder.DropIndex(
                name: "IX_Soldier_TripleNumber",
                table: "Soldier");

            migrationBuilder.DropIndex(
                name: "IX_Policemen_Name",
                table: "Policemen");

            migrationBuilder.RenameColumn(
                name: "SoldierId",
                table: "MissionSoldier",
                newName: "SoldiersId");

            migrationBuilder.RenameColumn(
                name: "MissionId",
                table: "MissionSoldier",
                newName: "MissionsId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionSoldier_SoldierId",
                table: "MissionSoldier",
                newName: "IX_MissionSoldier_SoldiersId");

            migrationBuilder.RenameColumn(
                name: "PoliceManId",
                table: "MissionPoliceMan",
                newName: "PolicemenId");

            migrationBuilder.RenameColumn(
                name: "MissionId",
                table: "MissionPoliceMan",
                newName: "MissionsId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionPoliceMan_PoliceManId",
                table: "MissionPoliceMan",
                newName: "IX_MissionPoliceMan_PolicemenId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionPoliceMan_Mission_MissionsId",
                table: "MissionPoliceMan",
                column: "MissionsId",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionPoliceMan_Policemen_PolicemenId",
                table: "MissionPoliceMan",
                column: "PolicemenId",
                principalTable: "Policemen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionSoldier_Mission_MissionsId",
                table: "MissionSoldier",
                column: "MissionsId",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionSoldier_Soldier_SoldiersId",
                table: "MissionSoldier",
                column: "SoldiersId",
                principalTable: "Soldier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
