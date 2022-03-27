using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Modules.Settlements.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Invo.Modules.Settlements.Infrastructure.EntityFramework.Repositories
{
    internal class PostgresCostInvoiceRepository : ICostInvoiceRepository
    {
        private readonly SettlementsDbContext _dbContext;
        private readonly DbSet<CostInvoice> _costInvoices;
        
        public PostgresCostInvoiceRepository(SettlementsDbContext dbContext)
        {
            _dbContext = dbContext;
            _costInvoices = dbContext.CostInvoices;
        }

        public async Task<IReadOnlyList<CostInvoice>> GetAsync(Guid buyerId)
            => await _costInvoices.Where(x => x.BuyerId.Equals(buyerId)).ToListAsync();

        public async Task<IReadOnlyList<CostInvoice>> GetAsync(Guid buyerId, int month, int year)
            => await _costInvoices
                .Where(x =>
                    x.BuyerId.Equals(buyerId) && x.DateOfIssue.Month.Equals(month) && x.DateOfIssue.Year.Equals(year))
                .ToListAsync();

        public async Task AddAsync(CostInvoice costInvoice)
        {
            await _costInvoices.AddAsync(costInvoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CostInvoice costInvoice)
        {
            _costInvoices.Update(costInvoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CostInvoice incomeInvoice)
        {
            _costInvoices.Remove(incomeInvoice);
            await _dbContext.SaveChangesAsync();
        }
    }
}