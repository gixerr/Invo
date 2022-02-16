using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.DTO;

namespace Invo.Modules.Invoices.Core.Services
{
    internal interface IInvoiceService
    {
        Task AddAsync(InvoiceAddUpdateDto dto);
        Task<InvoiceDetailsUpdateDto> GetAsync(Guid id);
        Task<IReadOnlyList<InvoiceGetDto>> BrowseAsync();
        Task<IReadOnlyList<InvoiceGetDto>> BrowseBySellerAsync(Guid id);
        Task UpdateAsync(InvoiceAddUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}