using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.DTO;
using Invo.Modules.Invoices.Core.Entities;
using Invo.Modules.Invoices.Core.Repositories;

namespace Invo.Modules.Invoices.Core.Services
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        
        public async Task AddAsync(InvoiceAddUpdateDto dto)
        {
            var sellerInvoices = await _invoiceRepository.BrowseBySellerAsync(dto.SellerId);
            if (sellerInvoices.Any(x => x.Number.Equals(dto.Number, StringComparison.OrdinalIgnoreCase)))
            {
                //TODO: change exception type
                throw new Exception();
            }
            dto.Id = Guid.NewGuid();
            var invoice = dto.ToInvoice();
            await _invoiceRepository.AddAsync(invoice);
        }
        
        public async Task<InvoiceDetailsUpdateDto> GetAsync(Guid id)
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

        public async Task UpdateAsync(InvoiceAddUpdateDto dto)
        {
            var invoice = await _invoiceRepository.GetAsync(dto.Id);
            if (invoice is null)
            {
                //TODO: change exception type
                throw new Exception();
            }

            invoice.Type = dto.Type;
            invoice.Number = dto.Number;
            invoice.DateOfIssue = dto.DateOfIssue;
            invoice.SaleDate = dto.SaleDate;
            invoice.SellerId = dto.SellerId;
            invoice.BuyerId = dto.BuyerId;
            invoice.Items = dto.Items.Select(x => x.ToInvoiceItem(dto.Id));
            invoice.NetAmount = dto.NetAmount;
            invoice.GrossAmount = dto.GrossAmount;

            await _invoiceRepository.UpdateAsync(invoice);
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetAsync(id);
            if (invoice is null)
            {
                //TODO: change exception type
                throw new Exception();
            }

            await _invoiceRepository.DeleteAsync(invoice);
        }
    }
}