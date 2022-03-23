using Microsoft.EntityFrameworkCore.Migrations;

namespace Invo.Modules.Settlements.Infrastructure.EntityFramework.Migrations
{
    public partial class Settlement_Module_Invoices_Alter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Number",
                schema: "settlements",
                table: "IncomeInvoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                schema: "settlements",
                table: "CostInvoices",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                schema: "settlements",
                table: "IncomeInvoices");

            migrationBuilder.DropColumn(
                name: "Number",
                schema: "settlements",
                table: "CostInvoices");
        }
    }
}
