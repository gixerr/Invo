using System;
using System.Collections.Generic;

namespace Invo.Modules.Invoices.Core.DTO
{
    internal class InvoiceDetailsDto : InvoiceGetDto
    {
        public DateTime SaleDate { get; set; }
        public Guid SellerId { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VatAmount { get; set; }
        public IReadOnlyList<InvoiceItemDto> Items { get; set; }
        public bool IsCarInvoice { get; set; }
    }
}