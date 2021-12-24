using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockTrader.Portfolios.Persistence.Migrations
{
    public partial class CreatePortfolio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "portfolios_write");

            migrationBuilder.CreateTable(
                name: "Portfolio",
                schema: "portfolios_write",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portfolio",
                schema: "portfolios_write");
        }
    }
}
