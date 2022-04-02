using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Settlements.Application.Services;
using Invo.Modules.Settlements.Domain.Repositories;
using Invo.Shared.Abstractions.Commands;

namespace Invo.Modules.Settlements.Application.Commands.Handlers
{
    public class CreateMonthSettlementHandler : ICommandHandler<CreateMonthSettlement>
    {
        private readonly IIncomeInvoiceRepository _invoiceRepository;
        private readonly ICostInvoiceRepository _costInvoiceRepository;
        private readonly ICostCalculationService _costCalculationService;
        private readonly ITaxCalculationService _taxCalculationService;

        public CreateMonthSettlementHandler(IIncomeInvoiceRepository invoiceRepository, ICostInvoiceRepository costInvoiceRepository,
            ICostCalculationService costCalculationService, ITaxCalculationService taxCalculationService)
        {
            _invoiceRepository = invoiceRepository;
            _costInvoiceRepository = costInvoiceRepository;
            _costCalculationService = costCalculationService;
            _taxCalculationService = taxCalculationService;
        }
        public async Task HandleAsync(CreateMonthSettlement command)
        {
            // var toSpent = totalIncome + totalIncomeVat - vatToPay - taxToPay;
            
            var monthIncomes = await _invoiceRepository.BrowseAsync(command.CompanyId, command.Month, command.Year);
            var costs = await _costInvoiceRepository.BrowseAsync(command.CompanyId, command.Month, command.Year);
            
            var monthIncome = monthIncomes.Sum(x => x.NetAmount);
            var monthIncomeVat = monthIncomes.Sum(x => x.VatAmount);
            var processedCosts = _costCalculationService.ProcessCosts(costs).ToList();
            var processedCostsAmount = processedCosts.Sum(x => x.CostAmount);
            var processedCostsVatAmount = processedCosts.Sum(x => x.VatAmount);
            var taxToPay = _taxCalculationService.GetTaxToPay(monthIncome, processedCostsAmount);
            var vatToPay = _taxCalculationService.GetVatToPay(monthIncomeVat, processedCostsVatAmount);
            var toSpent = _costCalculationService.GetToSpentValue(monthIncome, monthIncomeVat, taxToPay, vatToPay);
        }
    }
}