using System.Threading.Tasks;
using Invo.Modules.Settlements.Application.Exceptions;
using Invo.Modules.Settlements.Domain.Repositories;
using Invo.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace Invo.Modules.Settlements.Application.Events.External.Handlers
{
    internal class CostInvoiceDeletedHandler : IEventHandler<CostInvoiceDeleted>
    {
        private readonly ICostInvoiceRepository _costInvoiceRepository;
        private readonly ILogger<CostInvoiceDeletedHandler> _logger;

        public CostInvoiceDeletedHandler(ICostInvoiceRepository costInvoiceRepository,
            ILogger<CostInvoiceDeletedHandler> logger)
        {
            _costInvoiceRepository = costInvoiceRepository;
            _logger = logger;
        }

        public async Task HandleAsync(CostInvoiceDeleted @event)
        {
            if (@event is null)
            {
                throw new EventCannotBeNullException(nameof(@event));
            }

            var invoiceToDelete = await _costInvoiceRepository.GetAsync(@event.Id);
            if (invoiceToDelete is null)
            {
                throw new InvoiceNotFoundException(@event.Id);
            }

            await _costInvoiceRepository.DeleteAsync(invoiceToDelete);
            _logger.LogInformation($"Deleted cost invoice with number '{@invoiceToDelete.Number}' from settlement module.");
        }
    }
}