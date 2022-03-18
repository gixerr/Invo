using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.DTO;

namespace Invo.Modules.Invoices.Core.Services
{
    internal interface IIncomeInvoiceService
    {
        Task AddAsync(InvoiceAddDto dto);
        Task<InvoiceDetailsDto> GetAsync(Guid id);
        Task<IReadOnlyList<InvoiceGetDto>> BrowseAsync();
        Task<IReadOnlyList<InvoiceGetDto>> BrowseBySellerAsync(Guid id);
        Task UpdateAsync(InvoiceUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}