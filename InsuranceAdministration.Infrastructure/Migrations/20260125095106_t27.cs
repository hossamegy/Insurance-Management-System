using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoldierLeaveOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LeaveType = table.Column<string>(type: "TEXT", nullable: false),
                    SettingsOptionsId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldierLeaveOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoldierLeaveOptions_SettingsOptions_SettingsOptionsId",
                        column: x => x.SettingsOptionsId,
                        principalTable: "SettingsOptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoldierLeaveOptions_SettingsOptionsId",
                table: "SoldierLeaveOptions",
                column: "SettingsOptionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoldierLeaveOptions");
        }
    }
}
