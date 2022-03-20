using System;
using System.Collections.Generic;

namespace Invo.Modules.Invoices.Core.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid SellerId { get; set; }
        public Guid BuyerId { get; set; }
        public ICollection<InvoiceItem> Items { get; set; }
        public decimal VatAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
    }
}