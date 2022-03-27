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
    internal class IncomeInvoiceAddedHandler : IEventHandler<IncomeInvoiceAdded>
    {
        private readonly IIncomeInvoiceRepository _incomeInvoiceRepository;
        private readonly ILogger<IncomeInvoiceAddedHandler> _logger;

        public IncomeInvoiceAddedHandler(IIncomeInvoiceRepository incomeInvoiceRepository,
            ILogger<IncomeInvoiceAddedHandler> logger)
        {
            _incomeInvoiceRepository = incomeInvoiceRepository;
            _logger = logger;
        }
        
        public async Task HandleAsync(IncomeInvoiceAdded @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event),"Event can't be null.");
            }

            var sellerInvoices = await _incomeInvoiceRepository.GetAsync(@event.SellerId);

            if (sellerInvoices.Any(x => x.Number.Equals(@event.Number)))
            {
                throw new InvoiceAlreadyExistsException(@event.Number);
            }

            IncomeInvoice incomeInvoice = new()
            {
                Id = @event.Id,
                DateOfIssue = @event.DateOfIssue,
                NetAmount = @event.NetAmount,
                Number = @event.Number,
                SellerId = @event.SellerId,
                Type = @event.Type,
                VatAmount = @event.VatAmount
            };

            await _incomeInvoiceRepository.AddAsync(incomeInvoice);
            _logger.LogInformation($"Added income invoice with number: '{incomeInvoice.Number}' to settlements module.");
        }
    }
}