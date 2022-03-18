using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.Entities;
using Invo.Modules.Invoices.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Invo.Modules.Invoices.Core.DAL.Repositories
{
    internal class PostgresIncomeInvoiceRepository : IIncomeInvoiceRepository
    {
        private readonly InvoicesDbContext _dbContext;
        private readonly DbSet<IncomeInvoice> _incomeInvoices;

        public PostgresIncomeInvoiceRepository(InvoicesDbContext dbContext)
        {
            _dbContext = dbContext;
            _incomeInvoices = _dbContext.IncomeInvoices;
        }

        public async Task<IncomeInvoice> GetAsync(Guid id)
            => await _incomeInvoices.Include(x => x.Items).SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<IReadOnlyList<IncomeInvoice>> BrowseAsync()
            => await _incomeInvoices.ToListAsync();

        public async Task<IReadOnlyList<IncomeInvoice>> BrowseBySellerAsync(Guid id)
            => await _incomeInvoices.Where(x => x.SellerId.Equals(id)).ToListAsync();

        public async Task AddAsync(IncomeInvoice incomeInvoice)
        {
            await _incomeInvoices.AddAsync(incomeInvoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(IncomeInvoice incomeInvoice)
        {
            _incomeInvoices.Update(incomeInvoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(IncomeInvoice incomeInvoice)
        {
            _incomeInvoices.Remove(incomeInvoice);
            await _dbContext.SaveChangesAsync();
        }
    }
}