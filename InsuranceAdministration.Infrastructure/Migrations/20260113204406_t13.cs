using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Family",
                newName: "SpouseNationalId");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Family",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseAddress",
                table: "Family",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseFullName",
                table: "Family",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseJob",
                table: "Family",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Family");

            migrationBuilder.DropColumn(
                name: "SpouseAddress",
                table: "Family");

            migrationBuilder.DropColumn(
                name: "SpouseFullName",
                table: "Family");

            migrationBuilder.DropColumn(
                name: "SpouseJob",
                table: "Family");

            migrationBuilder.RenameColumn(
                name: "SpouseNationalId",
                table: "Family",
                newName: "Name");
        }
    }
}
