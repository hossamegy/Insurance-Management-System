using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SoldierLeave_SoldierId",
                table: "SoldierLeave");

            migrationBuilder.CreateIndex(
                name: "IX_SoldierLeave_SoldierId",
                table: "SoldierLeave",
                column: "SoldierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SoldierLeave_SoldierId",
                table: "SoldierLeave");

            migrationBuilder.CreateIndex(
                name: "IX_SoldierLeave_SoldierId",
                table: "SoldierLeave",
                column: "SoldierId",
                unique: true);
        }
    }
}
