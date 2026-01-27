using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dailyMealOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SettingsOptionsId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dailyMealOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dailyMealOptions_SettingsOptions_SettingsOptionsId",
                        column: x => x.SettingsOptionsId,
                        principalTable: "SettingsOptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_dailyMealOptions_SettingsOptionsId",
                table: "dailyMealOptions",
                column: "SettingsOptionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dailyMealOptions");
        }
    }
}
