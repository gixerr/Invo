using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invo.Modules.Settlements.Infrastructure.EntityFramework.Migrations
{
    public partial class Settlement_Module_MonthSettlement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthSettlements",
                schema: "settlements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Income = table.Column<decimal>(type: "numeric", nullable: false),
                    IncomeVat = table.Column<decimal>(type: "numeric", nullable: false),
                    Costs = table.Column<decimal>(type: "numeric", nullable: false),
                    CostsVat = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxToPay = table.Column<decimal>(type: "numeric", nullable: false),
                    VatToPay = table.Column<decimal>(type: "numeric", nullable: false),
                    ToSpent = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthSettlements", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthSettlements",
                schema: "settlements");
        }
    }
}
