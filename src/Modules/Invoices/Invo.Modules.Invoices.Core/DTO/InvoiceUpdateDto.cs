using System;
using System.ComponentModel.DataAnnotations;

namespace Invo.Modules.Invoices.Core.DTO
{
    internal class InvoiceUpdateDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public Guid BuyerId { get; set; }
        [Required]
        public Guid SellerId { get; set; }
        [Required]
        public DateTime DateOfIssue { get; set; }
        [Required]
        public DateTime SaleDate { get; set; }
        public bool IsCarInvoice { get; set; }
    }
}