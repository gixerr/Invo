using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invo.Modules.Settlements.Domain.Entities;

namespace Invo.Modules.Settlements.Domain.Repositories
{
    public interface IIncomeInvoiceRepository
    {
        Task<IReadOnlyList<IncomeInvoice>> GetAsync(Guid sellerId);
        Task<IReadOnlyList<IncomeInvoice>> GetAsync(Guid sellerId, int month, int year);
        Task AddAsync(IncomeInvoice incomeInvoice);
        Task UpdateAsync(IncomeInvoice incomeInvoice);
        Task DeleteAsync(IncomeInvoice incomeInvoice);
    }
}