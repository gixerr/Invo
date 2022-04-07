using System.Threading.Tasks;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Modules.Settlements.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Invo.Modules.Settlements.Infrastructure.EntityFramework.Repositories
{
    class PostgresMonthSettlementRepository : IMonthSettlementRepository
    {
        private readonly SettlementsDbContext _dbContext;
        private readonly DbSet<MonthSettlement> _monthSettlements;

        public PostgresMonthSettlementRepository(SettlementsDbContext dbContext)
        {
            _dbContext = dbContext;
            _monthSettlements = dbContext.MonthSettlements;
        }

        public async Task AddAsync(MonthSettlement monthSettlement)
        {
            await _monthSettlements.AddAsync(monthSettlement);
            await _dbContext.SaveChangesAsync();
        }
    }
}