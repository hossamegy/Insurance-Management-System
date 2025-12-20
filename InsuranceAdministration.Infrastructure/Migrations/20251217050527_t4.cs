using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SettingsOptionsId",
                table: "PoliceRankOptions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SettingsOptionsId",
                table: "EducationLevelOptions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SettingsOptionsId",
                table: "AssignmentOptions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SettingsOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoliceRankOptions_SettingsOptionsId",
                table: "PoliceRankOptions",
                column: "SettingsOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevelOptions_SettingsOptionsId",
                table: "EducationLevelOptions",
                column: "SettingsOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentOptions_SettingsOptionsId",
                table: "AssignmentOptions",
                column: "SettingsOptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentOptions_SettingsOptions_SettingsOptionsId",
                table: "AssignmentOptions",
                column: "SettingsOptionsId",
                principalTable: "SettingsOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationLevelOptions_SettingsOptions_SettingsOptionsId",
                table: "EducationLevelOptions",
                column: "SettingsOptionsId",
                principalTable: "SettingsOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PoliceRankOptions_SettingsOptions_SettingsOptionsId",
                table: "PoliceRankOptions",
                column: "SettingsOptionsId",
                principalTable: "SettingsOptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentOptions_SettingsOptions_SettingsOptionsId",
                table: "AssignmentOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationLevelOptions_SettingsOptions_SettingsOptionsId",
                table: "EducationLevelOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_PoliceRankOptions_SettingsOptions_SettingsOptionsId",
                table: "PoliceRankOptions");

            migrationBuilder.DropTable(
                name: "SettingsOptions");

            migrationBuilder.DropIndex(
                name: "IX_PoliceRankOptions_SettingsOptionsId",
                table: "PoliceRankOptions");

            migrationBuilder.DropIndex(
                name: "IX_EducationLevelOptions_SettingsOptionsId",
                table: "EducationLevelOptions");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentOptions_SettingsOptionsId",
                table: "AssignmentOptions");

            migrationBuilder.DropColumn(
                name: "SettingsOptionsId",
                table: "PoliceRankOptions");

            migrationBuilder.DropColumn(
                name: "SettingsOptionsId",
                table: "EducationLevelOptions");

            migrationBuilder.DropColumn(
                name: "SettingsOptionsId",
                table: "AssignmentOptions");
        }
    }
}
