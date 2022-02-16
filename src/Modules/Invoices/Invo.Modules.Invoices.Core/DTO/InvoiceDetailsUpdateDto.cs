using System;

namespace Invo.Modules.Invoices.Core.DTO
{
    public class InvoiceDetailsUpdateDto : InvoiceAddUpdateDto
    {
        public DateTime SaleDate { get; set; }
        public Guid SellerId { get; set; }
        public decimal NetAmount { get; set; }
    }
}