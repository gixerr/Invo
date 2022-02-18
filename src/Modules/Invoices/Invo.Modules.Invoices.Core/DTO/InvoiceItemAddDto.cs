using System;
using System.ComponentModel.DataAnnotations;
using Invo.Shared.Abstractions.Currency;

namespace Invo.Modules.Invoices.Core.DTO
{
    internal class InvoiceItemAddDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required] 
        public decimal NetPrice { get; set; }
        [Required]
        public int VatRate { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public decimal ExchangeRate { get; set; }
    }
}