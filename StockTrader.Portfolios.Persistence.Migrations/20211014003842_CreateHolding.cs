using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockTrader.Portfolios.Persistence.Migrations
{
    public partial class CreateHolding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Holding",
                schema: "portfolios_write",
                columns: table => new
                {
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShareCount = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holding", x => new { x.PortfolioId, x.Symbol });
                    table.ForeignKey(
                        name: "FK_Holding_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalSchema: "portfolios_write",
                        principalTable: "Portfolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holding",
                schema: "portfolios_write");
        }
    }
}
