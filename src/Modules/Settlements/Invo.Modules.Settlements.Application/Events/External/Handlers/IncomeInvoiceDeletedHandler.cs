using System;
using System.Threading.Tasks;
using Invo.Modules.Settlements.Application.Exceptions;
using Invo.Modules.Settlements.Domain.Repositories;
using Invo.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace Invo.Modules.Settlements.Application.Events.External.Handlers
{
    internal class IncomeInvoiceDeletedHandler : IEventHandler<IncomeInvoiceDeleted>
    {
        private readonly IIncomeInvoiceRepository _incomeInvoiceRepository;
        private readonly ILogger<IncomeInvoiceDeleted> _logger;

        public IncomeInvoiceDeletedHandler(IIncomeInvoiceRepository incomeInvoiceRepository,
            ILogger<IncomeInvoiceDeleted> logger)
        {
            _incomeInvoiceRepository = incomeInvoiceRepository;
            _logger = logger;
        }

        public async Task HandleAsync(IncomeInvoiceDeleted @event)
        {
            if (@event is null)
            {
                throw new EventCannotBeNullException(nameof(@event));
            }

            var invoiceToDelete = await _incomeInvoiceRepository.GetAsync(@event.Id);
            if (invoiceToDelete is null)
            {
                throw new InvoiceNotFoundException(@event.Id);
            }

            await _incomeInvoiceRepository.DeleteAsync(invoiceToDelete);
            _logger.LogInformation($"Deleted income invoice with number '{@invoiceToDelete.Number}' from settlement module.");
        }
    }
}