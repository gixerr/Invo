using System;

namespace Invo.Modules.Settlements.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public DateTime DateOfIssue { get; set; }
        public decimal VatAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
}