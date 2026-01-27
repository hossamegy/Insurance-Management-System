using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MissionType",
                table: "Mission",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissionType",
                table: "Mission");
        }
    }
}
