using System;
using System.Collections.Generic;
using Invo.Modules.Invoices.Core.DTO;
using Invo.Modules.Invoices.Core.Entities;

namespace Invo.Modules.Invoices.Core.Services
{
    internal interface IInvoiceItemsService
    {
        public ICollection<InvoiceItem> ProcessItems(IEnumerable<InvoiceItemAddDto> dtoItems, Guid invoiceId);
    }
}