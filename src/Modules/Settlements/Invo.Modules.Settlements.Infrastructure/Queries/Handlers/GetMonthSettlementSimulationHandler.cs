using System.Threading.Tasks;
using Invo.Modules.Settlements.Application.Factories;
using Invo.Modules.Settlements.Application.Queries;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Modules.Settlements.Domain.Repositories;
using Invo.Shared.Abstractions.Queries;

namespace Invo.Modules.Settlements.Infrastructure.Queries.Handlers
{
    public class GetMonthSettlementSimulationHandler : IQueryHandler<GetMonthSettlementSimulation, MonthSettlement>
    {
        private readonly IIncomeInvoiceRepository _invoiceRepository;
        private readonly ICostInvoiceRepository _costInvoiceRepository;
        private readonly IMonthSettlementFactory _monthSettlementFactory;

        public GetMonthSettlementSimulationHandler(IIncomeInvoiceRepository invoiceRepository,
            ICostInvoiceRepository costInvoiceRepository, IMonthSettlementFactory monthSettlementFactory)
        {
            _invoiceRepository = invoiceRepository;
            _costInvoiceRepository = costInvoiceRepository;
            _monthSettlementFactory = monthSettlementFactory;
        }

        public async Task<MonthSettlement> HandleAsync(GetMonthSettlementSimulation query)
        {
            
            var monthIncomes = await _invoiceRepository.BrowseAsync(query.CompanyId, query.Month, query.Year);
            var costs = await _costInvoiceRepository.BrowseAsync(query.CompanyId, query.Month, query.Year);

            var monthSettlement = _monthSettlementFactory.Create(monthIncomes, costs, query.CompanyId, query.Month,
                query.Year);

            return monthSettlement;
        }
    }
}