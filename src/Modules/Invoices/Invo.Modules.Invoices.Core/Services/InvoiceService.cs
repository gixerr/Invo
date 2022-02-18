using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.DTO;
using Invo.Modules.Invoices.Core.Entities;
using Invo.Modules.Invoices.Core.Exceptions;
using Invo.Modules.Invoices.Core.Repositories;
using Invo.Shared.Infrastructure.Services;

namespace Invo.Modules.Invoices.Core.Services
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemsService _invoiceItemsService;

        public InvoiceService(IInvoiceRepository invoiceRepository, IGrossNetCalculationService grossNetCalculationService, IInvoiceItemsService invoiceItemsService)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceItemsService = invoiceItemsService;
        }
        
        public async Task AddAsync(InvoiceAddDto dto)
        {
            var sellerInvoices = await _invoiceRepository.BrowseBySellerAsync(dto.SellerId);
            if (InvoiceAlreadyExist(sellerInvoices, dto.Number))
            {
                throw new InvoiceAlreadyExistException(dto.Number);
            }

            var invoice = CreateInvoice(dto);

            await _invoiceRepository.AddAsync(invoice);
        }
        
        public async Task<InvoiceDetailsDto> GetAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetAsync(id);
            var invoiceDetailsDto = invoice.ToInvoiceDetailsDto();

            return invoiceDetailsDto;
        }

        public async Task<IReadOnlyList<InvoiceGetDto>> BrowseAsync()
        {
            var invoices = await _invoiceRepository.BrowseAsync();
            var invoiceGetDtos = invoices.Select(x => x.ToInvoiceGetDto());

            return invoiceGetDtos.ToList();
        }

        public async Task<IReadOnlyList<InvoiceGetDto>> BrowseBySellerAsync(Guid id)
        {
            var invoices = await _invoiceRepository.BrowseBySellerAsync(id);
            var invoiceGetDtos = invoices.Select(x => x.ToInvoiceGetDto());

            return invoiceGetDtos.ToList();
        }

        public async Task UpdateAsync(InvoiceUpdateDto dto)
        {
            var invoice = await _invoiceRepository.GetAsync(dto.Id);
            if (invoice is null)
            {
                throw new InvoiceNotFoundException(dto.Id);
            }
            
            var sellerInvoices = await _invoiceRepository.BrowseBySellerAsync(dto.SellerId);
            if (InvoiceAlreadyExist(sellerInvoices, dto.Number))
            {
                throw new InvoiceAlreadyExistException(dto.Number);
            }

            invoice.Type = dto.Type;
            invoice.Number = dto.Number;
            invoice.DateOfIssue = dto.DateOfIssue;
            invoice.SaleDate = dto.SaleDate;
            invoice.SellerId = dto.SellerId;
            invoice.BuyerId = dto.BuyerId;
            //TODO: implement items update
            
            await _invoiceRepository.UpdateAsync(invoice);
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetAsync(id);
            if (invoice is null)
            {
                throw new InvoiceNotFoundException(id);
            }

            await _invoiceRepository.DeleteAsync(invoice);
        }

        private bool InvoiceAlreadyExist(IReadOnlyList<Invoice> invoices, string number)
        {
            var exist = invoices.Any(x => x.Number.Equals(number, StringComparison.OrdinalIgnoreCase));

            return exist;
        }

        private Invoice CreateInvoice(InvoiceAddDto dto)
        {
            dto.Id = Guid.NewGuid();
            Invoice invoice = new()
            {
                Id = dto.Id,
                Type = dto.Type,
                Number = dto.Number,
                DateOfIssue = dto.DateOfIssue,
                SaleDate = dto.SaleDate,
                SellerId = dto.SellerId,
                BuyerId = dto.BuyerId,
            };
            invoice.Items = _invoiceItemsService.ProcessItems(dto.Items, invoice.Id);
            invoice.NetAmount = GetRoundedAmount(invoice.Items.Sum(x => x.NetAmount));
            invoice.GrossAmount = GetRoundedAmount(invoice.Items.Sum(x => x.GrossAmount));
            invoice.VatAmount = GetRoundedAmount(invoice.Items.Sum(x => x.VatAmount));

            return invoice;
        }

        private static decimal GetRoundedAmount(decimal amount)
            => Math.Round(amount, 2);
    }
}