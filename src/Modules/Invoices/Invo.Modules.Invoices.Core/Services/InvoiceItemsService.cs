using System;
using System.Collections.Generic;
using System.Linq;
using Invo.Modules.Invoices.Core.DTO;
using Invo.Modules.Invoices.Core.Entities;
using Invo.Shared.Abstractions.Calculations;
using Invo.Shared.Infrastructure.Services;

namespace Invo.Modules.Invoices.Core.Services
{
    internal class InvoiceItemsService : IInvoiceItemsService
    {
        private readonly IGrossNetCalculationService _grossNetCalculationService;
        private readonly ICurrencyService _currencyService;

        public InvoiceItemsService(IGrossNetCalculationService grossNetCalculationService,
            ICurrencyService currencyService)
        {
            _grossNetCalculationService = grossNetCalculationService;
            _currencyService = currencyService;
        }

        public IEnumerable<InvoiceItem> ProcessItems(IEnumerable<InvoiceItemAddDto> dtoItems, Guid invoiceId)
        {
            var invoiceItems = dtoItems.Select(x => new InvoiceItem
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoiceId,
                Name = x.Name,
                Unit = x.Unit,
                Amount = x.Amount,
                NetPrice = Math.Round(_currencyService.ConvertToUserCurrency(x.NetPrice, x.Currency, x.ExchangeRate), 2),
                VatRate = x.VatRate,

                GrossPrice = _grossNetCalculationService
                    .GetGrossPrice(_currencyService
                        .ConvertToUserCurrency(x.NetPrice, x.Currency, x.ExchangeRate), x.Amount),

                NetAmount = _grossNetCalculationService
                    .GetNetAmount(_currencyService
                        .ConvertToUserCurrency(x.NetPrice, x.Currency, x.ExchangeRate), x.Amount),

                VatAmount = _grossNetCalculationService
                    .GetSummarisedVatAmount(_currencyService
                        .ConvertToUserCurrency(x.NetPrice, x.Currency, x.ExchangeRate), x.VatRate, x.Amount),

                GrossAmount = _grossNetCalculationService
                    .GetGrossAmount(_currencyService
                        .ConvertToUserCurrency(x.NetPrice, x.Currency, x.ExchangeRate), x.VatRate, x.Amount)
            });

            return invoiceItems;
        }
    }
}