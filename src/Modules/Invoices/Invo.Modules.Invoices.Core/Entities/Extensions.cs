using System.Linq;
using Invo.Modules.Invoices.Core.DTO;

namespace Invo.Modules.Invoices.Core.Entities
{
    internal static class Extensions
    {
        public static InvoiceDetailsDto ToInvoiceDetailsDto(this Invoice invoice)
            => new()
            {
                Id = invoice.Id,
                Type = invoice.Type,
                Number = invoice.Number,
                DateOfIssue = invoice.DateOfIssue,
                SaleDate = invoice.SaleDate,
                SellerId = invoice.SellerId,
                BuyerId = invoice.BuyerId,
                Items = invoice.Items.Select(x => x.ToInvoiceItemDto()).ToList(),
                NetAmount = invoice.NetAmount,
                VatAmount = invoice.VatAmount,
                GrossAmount = invoice.GrossAmount
            };

        public static InvoiceGetDto ToInvoiceGetDto(this Invoice invoice)
            => new()
            {
                Id = invoice.Id,
                Type = invoice.Type,
                Number = invoice.Number,
                BuyerId = invoice.BuyerId,
                DateOfIssue = invoice.DateOfIssue,
                GrossAmount = invoice.GrossAmount
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