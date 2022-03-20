using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.DAL.Repositories;
using Invo.Modules.Invoices.Core.DTO;
using Invo.Modules.Invoices.Core.Entities;
using Invo.Modules.Invoices.Core.Repositories;

namespace Invo.Modules.Invoices.Core.Services
{
    internal class IncomeInvoiceService : IIncomeInvoiceService
    {
        private readonly IIncomeInvoiceRepository _incomeInvoiceRepository;
        private readonly IInvoiceItemsService _invoiceItemsService;

        public IncomeInvoiceService(IIncomeInvoiceRepository incomeInvoiceRepository,
            IInvoiceItemsService invoiceItemsService)
        {
            _incomeInvoiceRepository = incomeInvoiceRepository;
            _invoiceItemsService = invoiceItemsService;
        }

        public async Task AddAsync(InvoiceAddDto dto)
        {
            await _incomeInvoiceRepository.ThrowIfInvoiceExistAsync(dto);

            var invoice = CreateInvoice(dto);

            await _incomeInvoiceRepository.AddAsync(invoice);
        }

        public async Task<InvoiceDetailsDto> GetAsync(Guid id)
        {
            var invoice = await _incomeInvoiceRepository.GetInvoiceOrThrowAsync(id);
            var invoiceDetailsDto = invoice.ToInvoiceDetailsDto();

            return invoiceDetailsDto;
        }

        public async Task<IReadOnlyList<InvoiceGetDto>> BrowseAsync()
        {
            var invoices = await _incomeInvoiceRepository.BrowseAsync();
            var invoiceGetDtos = invoices.Select(x => x.ToInvoiceGetDto());

            return invoiceGetDtos.ToList();
        }

        public async Task<IReadOnlyList<InvoiceGetDto>> BrowseBySellerAsync(Guid id)
        {
            var invoices = await _incomeInvoiceRepository.BrowseBySellerAsync(id);
            var invoiceGetDtos = invoices.Select(x => x.ToInvoiceGetDto());

            return invoiceGetDtos.ToList();
        }

        public async Task UpdateAsync(InvoiceUpdateDto dto)
        {
            var invoice = await _incomeInvoiceRepository.GetInvoiceOrThrowAsync(dto.Id);
            if (!invoice.Number.Equals(dto.Number))
            {
                await _incomeInvoiceRepository.ThrowIfInvoiceExistAsync(dto);
            }

            invoice.Type = dto.Type;
            invoice.Number = dto.Number;
            invoice.DateOfIssue = dto.DateOfIssue;
            invoice.SaleDate = dto.SaleDate;
            invoice.SellerId = dto.SellerId;
            invoice.BuyerId = dto.BuyerId;
            //TODO: implement items update

            await _incomeInvoiceRepository.UpdateAsync(invoice);
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _incomeInvoiceRepository.GetInvoiceOrThrowAsync(id);

            await _incomeInvoiceRepository.DeleteAsync(invoice);
        }

        private IncomeInvoice CreateInvoice(InvoiceAddDto dto)
        {
            dto.Id = Guid.NewGuid();
            IncomeInvoice incomeInvoice = new()
            {
                Id = dto.Id,
                Type = dto.Type,
                Number = dto.Number,
                DateOfIssue = dto.DateOfIssue,
                SaleDate = dto.SaleDate,
                SellerId = dto.SellerId,
                BuyerId = dto.BuyerId,
            };
            incomeInvoice.Items = _invoiceItemsService.ProcessItems(dto.Items, incomeInvoice.Id);
            incomeInvoice.NetAmount = GetRoundedAmount(incomeInvoice.Items.Sum(x => x.NetAmount));
            incomeInvoice.GrossAmount = GetRoundedAmount(incomeInvoice.Items.Sum(x => x.GrossAmount));
            incomeInvoice.VatAmount = GetRoundedAmount(incomeInvoice.Items.Sum(x => x.VatAmount));

            return incomeInvoice;
        }

        private static decimal GetRoundedAmount(decimal amount)
            => Math.Round(amount, 2);
    }
}