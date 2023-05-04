using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackMachine.Logic.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SnackMachine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OneCentCount = table.Column<int>(type: "integer", nullable: false),
                    QuarterCount = table.Column<int>(type: "integer", nullable: false),
                    OneDollarCount = table.Column<int>(type: "integer", nullable: false),
                    FiveDollarCount = table.Column<int>(type: "integer", nullable: false),
                    TwentyDollarCount = table.Column<int>(type: "integer", nullable: false),
                    TenCentCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnackMachine", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnackMachine");
        }
    }
}
