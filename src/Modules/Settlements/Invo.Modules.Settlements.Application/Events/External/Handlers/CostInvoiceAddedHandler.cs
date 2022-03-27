using System;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Settlements.Application.Exceptions;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Modules.Settlements.Domain.Repositories;
using Invo.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace Invo.Modules.Settlements.Application.Events.External.Handlers
{
    internal class CostInvoiceAddedHandler : IEventHandler<CostInvoiceAdded>
    {
        private readonly ICostInvoiceRepository _costInvoiceRepository;
        private readonly ILogger<CostInvoiceAddedHandler> _logger;

        public CostInvoiceAddedHandler(ICostInvoiceRepository costInvoiceRepository,
            ILogger<CostInvoiceAddedHandler> logger)
        {
            _costInvoiceRepository = costInvoiceRepository;
            _logger = logger;
        }

        public async Task HandleAsync(CostInvoiceAdded @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event), "Event can't be null.");
            }
            
            var buyerInvoices = await _costInvoiceRepository.GetAsync(@event.BuyerId);

            if (buyerInvoices.Any(x => x.Number.Equals(@event.Number)))
            {
                throw new InvoiceAlreadyExistsException(@event.Number);
            }

            CostInvoice costInvoice = new()
            {
                Id = @event.Id,
                BuyerId = @event.BuyerId,
                DateOfIssue = @event.DateOfIssue,
                IsCarInvoice = @event.IsCarInvoice,
                NetAmount = @event.NetAmount,
                Number = @event.Number,
                Type = @event.Type,
                VatAmount = @event.VatAmount
            };

            await _costInvoiceRepository.AddAsync(costInvoice);
            _logger.LogInformation($"Added cost invoice with number: '{costInvoice.Number}' to settlements module.");
        }
    }
}