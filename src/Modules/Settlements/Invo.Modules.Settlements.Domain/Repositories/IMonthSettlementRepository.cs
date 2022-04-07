using System.Threading.Tasks;
using Invo.Modules.Settlements.Domain.Entities;

namespace Invo.Modules.Settlements.Domain.Repositories
{
    public interface IMonthSettlementRepository
    {
        Task AddAsync(MonthSettlement monthSettlement);
    }
}