using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.Entities;
using Invo.Modules.Invoices.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Invo.Modules.Invoices.Core.DAL.Repositories
{
    internal class PostgresCostInvoiceRepository : ICostInvoiceRepository
    {
        private readonly InvoicesDbContext _dbContext;
        private readonly DbSet<CostInvoice> _costInvoices;

        public PostgresCostInvoiceRepository(InvoicesDbContext dbContext)
        {
            _dbContext = dbContext;
            _costInvoices = _dbContext.CostInvoices;
        }

        public async Task<CostInvoice> GetAsync(Guid id)
            => await _costInvoices.Include(x => x.Items).SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<IReadOnlyList<CostInvoice>> BrowseAsync()
            => await _costInvoices.ToListAsync();

        public async Task<IReadOnlyList<CostInvoice>> BrowseBySellerAsync(Guid id)
            => await _costInvoices.Where(x => x.SellerId.Equals(id)).ToListAsync();

        public async Task AddAsync(CostInvoice incomeInvoice)
        {
            await _costInvoices.AddAsync(incomeInvoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CostInvoice incomeInvoice)
        {
            _costInvoices.Update(incomeInvoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CostInvoice incomeInvoice)
        {
            _costInvoices.Remove(incomeInvoice);
            await _dbContext.SaveChangesAsync();
        }
    }
}