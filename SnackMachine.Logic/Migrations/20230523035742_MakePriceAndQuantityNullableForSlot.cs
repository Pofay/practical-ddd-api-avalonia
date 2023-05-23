using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackMachine.Logic.Migrations
{
    /// <inheritdoc />
    public partial class MakePriceAndQuantityNullableForSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SnackPile_Quantity",
                table: "Slot",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "SnackPile_Price",
                table: "Slot",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SnackPile_Quantity",
                table: "Slot",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "SnackPile_Price",
                table: "Slot",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0m);
        }
    }
}
