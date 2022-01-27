using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockTrader.Portfolios.Projection.Sql.Migrations
{
    public partial class CreatePortfolioDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "portfolios_read");

            migrationBuilder.CreateTable(
                name: "PortfolioDetail",
                schema: "portfolios_read",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioDetail", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioDetail",
                schema: "portfolios_read");
        }
    }
}
