using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseFamily_AcquaintanceDocument_AcquaintanceDocumentId",
                table: "BaseFamily");

            migrationBuilder.DropForeignKey(
                name: "FK_Family_AcquaintanceDocument_AcquaintanceDocumentId",
                table: "Family");

            migrationBuilder.AlterColumn<int>(
                name: "AcquaintanceDocumentId",
                table: "Family",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AcquaintanceDocumentId",
                table: "BaseFamily",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseFamily_AcquaintanceDocument_AcquaintanceDocumentId",
                table: "BaseFamily",
                column: "AcquaintanceDocumentId",
                principalTable: "AcquaintanceDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Family_AcquaintanceDocument_AcquaintanceDocumentId",
                table: "Family",
                column: "AcquaintanceDocumentId",
                principalTable: "AcquaintanceDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseFamily_AcquaintanceDocument_AcquaintanceDocumentId",
                table: "BaseFamily");

            migrationBuilder.DropForeignKey(
                name: "FK_Family_AcquaintanceDocument_AcquaintanceDocumentId",
                table: "Family");

            migrationBuilder.AlterColumn<int>(
                name: "AcquaintanceDocumentId",
                table: "Family",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AcquaintanceDocumentId",
                table: "BaseFamily",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseFamily_AcquaintanceDocument_AcquaintanceDocumentId",
                table: "BaseFamily",
                column: "AcquaintanceDocumentId",
                principalTable: "AcquaintanceDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Family_AcquaintanceDocument_AcquaintanceDocumentId",
                table: "Family",
                column: "AcquaintanceDocumentId",
                principalTable: "AcquaintanceDocument",
                principalColumn: "Id");
        }
    }
}
