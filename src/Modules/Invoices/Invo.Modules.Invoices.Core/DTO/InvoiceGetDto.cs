using System;

namespace Invo.Modules.Invoices.Core.DTO
{
    public class InvoiceGetDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public Guid BuyerId { get; set; }
        public DateTime DateOfIssue { get; set; }
        public decimal GrossAmount { get; set; }
    }
}