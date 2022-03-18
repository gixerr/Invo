using System.Linq;
using Invo.Modules.Invoices.Core.DTO;

namespace Invo.Modules.Invoices.Core.Entities
{
    internal static class Extensions
    {
        public static InvoiceDetailsDto ToInvoiceDetailsDto(this IncomeInvoice incomeInvoice)
            => new()
            {
                Id = incomeInvoice.Id,
                Type = incomeInvoice.Type,
                Number = incomeInvoice.Number,
                DateOfIssue = incomeInvoice.DateOfIssue,
                SaleDate = incomeInvoice.SaleDate,
                SellerId = incomeInvoice.SellerId,
                BuyerId = incomeInvoice.BuyerId,
                Items = incomeInvoice.Items.Select(x => x.ToInvoiceItemDto()).ToList(),
                NetAmount = incomeInvoice.NetAmount,
                VatAmount = incomeInvoice.VatAmount,
                GrossAmount = incomeInvoice.GrossAmount
            };

        public static InvoiceGetDto ToInvoiceGetDto(this IncomeInvoice incomeInvoice)
            => new()
            {
                Id = incomeInvoice.Id,
                Type = incomeInvoice.Type,
                Number = incomeInvoice.Number,
                BuyerId = incomeInvoice.BuyerId,
                DateOfIssue = incomeInvoice.DateOfIssue,
                GrossAmount = incomeInvoice.GrossAmount
            };


        public static InvoiceItemDto ToInvoiceItemDto(this InvoiceItem invoiceItem)
            => new()
            {
                Id = invoiceItem.Id,
                InvoiceId = invoiceItem.InvoiceId,
                Name = invoiceItem.Name,
                Unit = invoiceItem.Unit,
                Amount = invoiceItem.Amount,
                NetPrice = invoiceItem.NetPrice,
                VatRate = invoiceItem.VatRate,
                NetAmount = invoiceItem.NetAmount,
                VatAmount = invoiceItem.VatAmount,
                GrossAmount = invoiceItem.GrossAmount,
                GrossPrice = invoiceItem.GrossPrice
                
            };
    }
}