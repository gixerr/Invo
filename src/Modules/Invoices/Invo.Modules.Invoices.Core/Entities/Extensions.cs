using System;
using System.Linq;
using Invo.Modules.Invoices.Core.DTO;

namespace Invo.Modules.Invoices.Core.Entities
{
    internal static class Extensions
    {
        public static InvoiceDetailsUpdateDto ToInvoiceDetailsDto(this Invoice invoice)
            => new()
            {
                Id = invoice.Id,
                Type = invoice.Type,
                Number = invoice.Number,
                DateOfIssue = invoice.DateOfIssue,
                SaleDate = invoice.SaleDate,
                SellerId = invoice.SellerId,
                BuyerId = invoice.BuyerId,
                Items = invoice.Items.Select(x => x.ToInvoiceItemDto()),
                NetAmount = invoice.NetAmount,
                GrossAmount = invoice.GrossAmount
            };

        public static Invoice ToInvoice(this InvoiceAddUpdateDto dto)
            => new()
            {
                Id = dto.Id,
                Type = dto.Type,
                Number = dto.Number,
                DateOfIssue = dto.DateOfIssue,
                SaleDate = dto.SaleDate,
                SellerId = dto.SellerId,
                BuyerId = dto.BuyerId,
                Items = dto.Items.Select(x => x.ToInvoiceItem(dto.Id)),
                NetAmount = dto.NetAmount,
                GrossAmount = dto.GrossAmount
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
                NetAmount = invoiceItem.NetAmount,
                VatRate = invoiceItem.VatRate,
                VatAmount = invoiceItem.VatAmount,
                GrossAmount = invoiceItem.GrossAmount,
                
            };
        
        public static InvoiceItem ToInvoiceItem(this InvoiceItemDto dto, Guid invoiceId)
            => new()
            {
                Id = dto.Id.Equals(Guid.Empty) ? Guid.NewGuid() : dto.Id,
                InvoiceId = invoiceId,
                Name = dto.Name,
                Unit = dto.Unit,
                Amount = dto.Amount,
                NetAmount = dto.NetAmount,
                VatRate = dto.VatRate,
                VatAmount = dto.VatAmount,
                GrossAmount = dto.GrossAmount,
                
            };
    }
}