using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackMachine.Logic.Migrations
{
    /// <inheritdoc />
    public partial class SchemaUpdateForSnackAndSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Snack",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snack", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SnackPile_SnackId = table.Column<Guid>(type: "uuid", nullable: false),
                    SnackPile_Quantity = table.Column<int>(type: "integer", nullable: false),
                    SnackPile_Price = table.Column<decimal>(type: "numeric", nullable: false),
                    SnackMachineId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    SnackMachineEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slot_SnackMachine_SnackMachineEntityId",
                        column: x => x.SnackMachineEntityId,
                        principalTable: "SnackMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slot_SnackMachine_SnackMachineId",
                        column: x => x.SnackMachineId,
                        principalTable: "SnackMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slot_Snack_SnackPile_SnackId",
                        column: x => x.SnackPile_SnackId,
                        principalTable: "Snack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slot_SnackMachineEntityId",
                table: "Slot",
                column: "SnackMachineEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_SnackMachineId",
                table: "Slot",
                column: "SnackMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_SnackPile_SnackId",
                table: "Slot",
                column: "SnackPile_SnackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropTable(
                name: "Snack");
        }
    }
}
