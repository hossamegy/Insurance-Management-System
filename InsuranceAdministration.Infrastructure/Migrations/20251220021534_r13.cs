using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class r13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoliceRankOptions");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Mission",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Mission");

            migrationBuilder.CreateTable(
                name: "PoliceRankOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SettingsOptionsId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceRankOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceRankOptions_SettingsOptions_SettingsOptionsId",
                        column: x => x.SettingsOptionsId,
                        principalTable: "SettingsOptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoliceRankOptions_SettingsOptionsId",
                table: "PoliceRankOptions",
                column: "SettingsOptionsId");
        }
    }
}
