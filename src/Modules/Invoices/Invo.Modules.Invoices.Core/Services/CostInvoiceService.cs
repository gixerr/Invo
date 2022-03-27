using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.DAL.Repositories;
using Invo.Modules.Invoices.Core.DTO;
using Invo.Modules.Invoices.Core.Entities;
using Invo.Modules.Invoices.Core.Events;
using Invo.Modules.Invoices.Core.Repositories;
using Invo.Shared.Abstractions.Messaging;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace Invo.Modules.Invoices.Core.Services
{
    internal class CostInvoiceService : ICostInvoiceService
    {
        private readonly ICostInvoiceRepository _costInvoiceRepository;
        private readonly IInvoiceItemsService _invoiceItemsService;
        private readonly IMessageBroker _messageBroker;

        public CostInvoiceService(ICostInvoiceRepository costInvoiceRepository, IInvoiceItemsService invoiceItemsService,
            IMessageBroker messageBroker)
        {
            _costInvoiceRepository = costInvoiceRepository;
            _invoiceItemsService = invoiceItemsService;
            _messageBroker = messageBroker;
        }
        
        public async Task AddAsync(InvoiceAddDto dto)
        {
            await _costInvoiceRepository.ThrowIfInvoiceExistAsync(dto);
            
            var invoice = CreateCostInvoice(dto);

            await _costInvoiceRepository.AddAsync(invoice);
            await _messageBroker.PublishAsync(new CostInvoiceAdded(invoice.Id, invoice.BuyerId, invoice.IsCarInvoice,
                invoice.Type, invoice.DateOfIssue, invoice.VatAmount, invoice.NetAmount, invoice.Number));
        }
        
        public async Task<InvoiceDetailsDto> GetAsync(Guid id)
        {
            var invoice = await _costInvoiceRepository.GetInvoiceOrThrowAsync(id);
            var invoiceDetailsDto = invoice.ToInvoiceDetailsDto();

            return invoiceDetailsDto;
        }

        public async Task<IReadOnlyList<InvoiceGetDto>> BrowseAsync()
        {
            var invoices = await _costInvoiceRepository.BrowseAsync();
            var invoiceGetDtos = invoices.Select(x => x.ToInvoiceGetDto());

            return invoiceGetDtos.ToList();
        }

        public async Task<IReadOnlyList<InvoiceGetDto>> BrowseBySellerAsync(Guid id)
        {
            var invoices = await _costInvoiceRepository.BrowseBySellerAsync(id);
            var invoiceGetDtos = invoices.Select(x => x.ToInvoiceGetDto());

            return invoiceGetDtos.ToList();
        }

        public async Task UpdateAsync(InvoiceUpdateDto dto)
        {
            var invoice = await _costInvoiceRepository.GetInvoiceOrThrowAsync(dto.Id);
            if (!invoice.Number.Equals(dto.Number))
            {
                await _costInvoiceRepository.ThrowIfInvoiceExistAsync(dto);
            }
            
            invoice.Type = dto.Type;
            invoice.Number = dto.Number;
            invoice.DateOfIssue = dto.DateOfIssue;
            invoice.SaleDate = dto.SaleDate;
            invoice.SellerId = dto.SellerId;
            invoice.BuyerId = dto.BuyerId;
            invoice.IsCarInvoice = dto.IsCarInvoice;
            //TODO: implement items update
            
            await _costInvoiceRepository.UpdateAsync(invoice);
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _costInvoiceRepository.GetInvoiceOrThrowAsync(id);
            await _costInvoiceRepository.DeleteAsync(invoice);
        }

        private CostInvoice CreateCostInvoice(InvoiceAddDto dto)
        {
            dto.Id = Guid.NewGuid();
            CostInvoice costInvoice = new()
            {
                Id = dto.Id,
                Type = dto.Type,
                Number = dto.Number,
                DateOfIssue = dto.DateOfIssue,
                SaleDate = dto.SaleDate,
                SellerId = dto.SellerId,
                BuyerId = dto.BuyerId,
                IsCarInvoice = dto.IsCarInvoice
            };
            costInvoice.Items = _invoiceItemsService.ProcessItems(dto.Items, costInvoice.Id);
            costInvoice.NetAmount = GetRoundedAmount(costInvoice.Items.Sum(x => x.NetAmount));
            costInvoice.GrossAmount = GetRoundedAmount(costInvoice.Items.Sum(x => x.GrossAmount));
            costInvoice.VatAmount = GetRoundedAmount(costInvoice.Items.Sum(x => x.VatAmount));

            return costInvoice;
        }

        private static decimal GetRoundedAmount(decimal amount)
            => Math.Round(amount, 2);
    }
}