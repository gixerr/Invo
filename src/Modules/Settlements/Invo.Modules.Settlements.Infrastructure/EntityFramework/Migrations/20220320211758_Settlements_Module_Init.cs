using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invo.Modules.Settlements.Infrastructure.EntityFramework.Migrations
{
    public partial class Settlements_Module_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "settlements");

            migrationBuilder.CreateTable(
                name: "CostInvoices",
                schema: "settlements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCarInvoice = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    DateOfIssue = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    VatAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostInvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeInvoices",
                schema: "settlements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    DateOfIssue = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    VatAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeInvoices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostInvoices",
                schema: "settlements");

            migrationBuilder.DropTable(
                name: "IncomeInvoices",
                schema: "settlements");
        }
    }
}
