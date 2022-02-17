using System;
using System.Collections.Generic;

namespace Invo.Modules.Invoices.Core.DTO
{
    internal class InvoiceDetailsDto : InvoiceGetDto
    {
        public DateTime SaleDate { get; set; }
        public Guid SellerId { get; set; }
        public decimal NetAmount { get; set; }
        public IReadOnlyList<InvoiceItemDto> Items { get; set; }
    }
}