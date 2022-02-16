﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Invo.Modules.Invoices.Core.DTO
{
    public class InvoiceAddUpdateDto
    {
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
        [Required]
        [MinLength(1, ErrorMessage = "Items cannot be empty.")]
        public IEnumerable<InvoiceItemDto> Items { get; set; }
        [Required]
        public decimal NetAmount { get; set; }
        [Required]
        public decimal GrossAmount { get; set; }
    }
}