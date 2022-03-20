using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Invo.Modules.Invoices.Core.DTO;
using Invo.Modules.Invoices.Core.Entities;
using Invo.Modules.Invoices.Core.Exceptions;
using Invo.Modules.Invoices.Core.Repositories;

namespace Invo.Modules.Invoices.Core.Services
{
    internal class CostInvoiceService : ICostInvoiceService
    {
        private readonly ICostInvoiceRepository _costInvoiceRepository;
        private readonly IInvoiceItemsService _invoiceItemsService;

        public CostInvoiceService(ICostInvoiceRepository costInvoiceRepository, IInvoiceItemsService invoiceItemsService)
        {
            _costInvoiceRepository = costInvoiceRepository;
            _invoiceItemsService = invoiceItemsService;
        }
        
        public async Task AddAsync(InvoiceAddDto dto)
        {
            var sellerInvoices = await _costInvoiceRepository.BrowseBySellerAsync(dto.SellerId);
            if (InvoiceAlreadyExist(sellerInvoices, dto.Number))
            {
                throw new InvoiceAlreadyExistException(dto.Number);
            }

            var invoice = CreateCostInvoice(dto);

            await _costInvoiceRepository.AddAsync(invoice);
        }
        
        public async Task<InvoiceDetailsDto> GetAsync(Guid id)
        {
            var invoice = await _costInvoiceRepository.GetAsync(id);
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
            var invoice = await _costInvoiceRepository.GetAsync(dto.Id);
            if (invoice is null)
            {
                throw new InvoiceNotFoundException(dto.Id);
            }
            
            var sellerInvoices = await _costInvoiceRepository.BrowseBySellerAsync(dto.SellerId);
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
            
            await _costInvoiceRepository.UpdateAsync(invoice);
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _costInvoiceRepository.GetAsync(id);
            if (invoice is null)
            {
                throw new InvoiceNotFoundException(id);
            }

            await _costInvoiceRepository.DeleteAsync(invoice);
        }

        private bool InvoiceAlreadyExist(IReadOnlyList<Invoice> invoices, string number)
        {
            var exist = invoices.Any(x => x.Number.Equals(number, StringComparison.OrdinalIgnoreCase));

            return exist;
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