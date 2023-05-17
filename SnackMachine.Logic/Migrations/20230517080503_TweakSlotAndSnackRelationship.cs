using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackMachine.Logic.Migrations
{
    /// <inheritdoc />
    public partial class TweakSlotAndSnackRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Slot_SnackPile_SnackId",
                table: "Slot");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_SnackPile_SnackId",
                table: "Slot",
                column: "SnackPile_SnackId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Slot_SnackPile_SnackId",
                table: "Slot");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_SnackPile_SnackId",
                table: "Slot",
                column: "SnackPile_SnackId");
        }
    }
}
