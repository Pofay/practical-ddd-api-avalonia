using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackMachine.Logic.Migrations
{
    /// <inheritdoc />
    public partial class SchemaUpdateForSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slot_Snack_SnackPile_SnackId",
                table: "Slot");

            migrationBuilder.AlterColumn<Guid>(
                name: "SnackPile_SnackId",
                table: "Slot",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Slot_Snack_SnackPile_SnackId",
                table: "Slot",
                column: "SnackPile_SnackId",
                principalTable: "Snack",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slot_Snack_SnackPile_SnackId",
                table: "Slot");

            migrationBuilder.AlterColumn<Guid>(
                name: "SnackPile_SnackId",
                table: "Slot",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Slot_Snack_SnackPile_SnackId",
                table: "Slot",
                column: "SnackPile_SnackId",
                principalTable: "Snack",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
