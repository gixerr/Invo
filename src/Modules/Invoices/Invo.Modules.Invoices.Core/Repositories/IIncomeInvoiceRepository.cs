using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.Entities;

namespace Invo.Modules.Invoices.Core.Repositories
{
    internal interface IIncomeInvoiceRepository
    {
        Task<IncomeInvoice> GetAsync(Guid id);
        Task<IReadOnlyList<IncomeInvoice>> BrowseAsync();
        Task<IReadOnlyList<IncomeInvoice>> BrowseBySellerAsync(Guid id);
        Task AddAsync(IncomeInvoice incomeInvoice);
        Task UpdateAsync(IncomeInvoice incomeInvoice);
        Task DeleteAsync(IncomeInvoice incomeInvoice);
    }
}