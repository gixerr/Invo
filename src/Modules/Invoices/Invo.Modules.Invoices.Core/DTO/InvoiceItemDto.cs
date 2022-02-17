using System;
using System.ComponentModel.DataAnnotations;

namespace Invo.Modules.Invoices.Core.DTO
{
    internal class InvoiceItemDto
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        
        public string Name { get; set; }
        
        public string Unit { get; set; }
        
        public decimal NetPrice { get; set; }

        public decimal GrossPrice { get; set; }
        
        public decimal Amount { get; set; }
        
        public decimal NetAmount { get; set; }
        
        public int VatRate { get; set; }
        
        public decimal VatAmount { get; set; }
        
        public decimal GrossAmount { get; set; }
    }
}