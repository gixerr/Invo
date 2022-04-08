using System.Threading.Tasks;
using Invo.Modules.Settlements.Application.Factories;
using Invo.Modules.Settlements.Domain.Repositories;
using Invo.Shared.Abstractions.Commands;

namespace Invo.Modules.Settlements.Application.Commands.Handlers
{
    public class CreateMonthSettlementHandler : ICommandHandler<CreateMonthSettlement>
    {
        private readonly IIncomeInvoiceRepository _invoiceRepository;
        private readonly ICostInvoiceRepository _costInvoiceRepository;
        private readonly IMonthSettlementRepository _monthSettlementRepository;
        private readonly IMonthSettlementFactory _monthSettlementFactory;

        public CreateMonthSettlementHandler(IIncomeInvoiceRepository invoiceRepository, ICostInvoiceRepository costInvoiceRepository,
            IMonthSettlementRepository monthSettlementRepository, IMonthSettlementFactory monthSettlementFactory)
        {
            _invoiceRepository = invoiceRepository;
            _costInvoiceRepository = costInvoiceRepository;
            _monthSettlementRepository = monthSettlementRepository;
            _monthSettlementFactory = monthSettlementFactory;
        }
        public async Task HandleAsync(CreateMonthSettlement command)
        {
            var monthIncomes = await _invoiceRepository.BrowseAsync(command.CompanyId, command.Month, command.Year);
            var costs = await _costInvoiceRepository.BrowseAsync(command.CompanyId, command.Month, command.Year);

            var monthSettlement = _monthSettlementFactory.Create(monthIncomes, costs, command.CompanyId, command.Month,
                command.Year);

            await _monthSettlementRepository.AddAsync(monthSettlement);
        }
    }
}