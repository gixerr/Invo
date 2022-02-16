using System;
using System.ComponentModel.DataAnnotations;

namespace Invo.Modules.Invoices.Core.DTO
{
    public class InvoiceItemDto
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        public string Unit { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public decimal NetAmount { get; set; }
        
        [Required]
        public int VatRate { get; set; }
        
        [Required]
        public decimal VatAmount { get; set; }
        
        [Required]
        public decimal GrossAmount { get; set; }
    }
}