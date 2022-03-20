using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invo.Modules.Invoices.Core.DAL.Migrations
{
    public partial class Invoices_Module_Add_CostInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CostInvoiceId",
                schema: "invoices",
                table: "InvoiceItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CostInvoices",
                schema: "invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCarInvoice = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Number = table.Column<string>(type: "text", nullable: true),
                    DateOfIssue = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    VatAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostInvoices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_CostInvoiceId",
                schema: "invoices",
                table: "InvoiceItems",
                column: "CostInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_CostInvoices_CostInvoiceId",
                schema: "invoices",
                table: "InvoiceItems",
                column: "CostInvoiceId",
                principalSchema: "invoices",
                principalTable: "CostInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_CostInvoices_CostInvoiceId",
                schema: "invoices",
                table: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "CostInvoices",
                schema: "invoices");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_CostInvoiceId",
                schema: "invoices",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "CostInvoiceId",
                schema: "invoices",
                table: "InvoiceItems");
        }
    }
}
