using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.DTO;
using Invo.Modules.Invoices.Core.Entities;
using Invo.Modules.Invoices.Core.Exceptions;
using Invo.Modules.Invoices.Core.Repositories;

namespace Invo.Modules.Invoices.Core.DAL.Repositories
{
    internal static class Extensions
    {
        public static async Task ThrowIfInvoiceExistAsync(this ICostInvoiceRepository costInvoiceRepository, InvoiceAddDto dto)
        {
            var sellerInvoices = await costInvoiceRepository.BrowseBySellerAsync(dto.SellerId);
            if (InvoiceAlreadyExist(sellerInvoices, dto.Number))
            {
                throw new InvoiceAlreadyExistException(dto.Number);
            }
        }
        
        public static async Task ThrowIfInvoiceExistAsync(this ICostInvoiceRepository costInvoiceRepository, InvoiceUpdateDto dto)
        {
            var sellerInvoices = await costInvoiceRepository.BrowseBySellerAsync(dto.SellerId);
            if (InvoiceAlreadyExist(sellerInvoices, dto.Number))
            {
                throw new InvoiceAlreadyExistException(dto.Number);
            }
        }
        
        public static async Task ThrowIfInvoiceExistAsync(this IIncomeInvoiceRepository costInvoiceRepository, InvoiceAddDto dto)
        {
            var sellerInvoices = await costInvoiceRepository.BrowseBySellerAsync(dto.SellerId);
            if (InvoiceAlreadyExist(sellerInvoices, dto.Number))
            {
                throw new InvoiceAlreadyExistException(dto.Number);
            }
        }
        
        public static async Task ThrowIfInvoiceExistAsync(this IIncomeInvoiceRepository costInvoiceRepository, InvoiceUpdateDto dto)
        {
            var sellerInvoices = await costInvoiceRepository.BrowseBySellerAsync(dto.SellerId);
            if (InvoiceAlreadyExist(sellerInvoices, dto.Number))
            {
                throw new InvoiceAlreadyExistException(dto.Number);
            }
        }
        
        public static async Task<CostInvoice> GetInvoiceOrThrowAsync(this ICostInvoiceRepository costInvoiceRepository, Guid id)
        {
            var invoice = await costInvoiceRepository.GetAsync(id);
            if (invoice is null)
            {
                throw new InvoiceNotFoundException(id);
            }

            return invoice;
        }
        
        public static async Task<IncomeInvoice> GetInvoiceOrThrowAsync(this IIncomeInvoiceRepository incomeInvoiceRepository, Guid id)
        {
            var invoice = await incomeInvoiceRepository.GetAsync(id);
            if (invoice is null)
            {
                throw new InvoiceNotFoundException(id);
            }

            return invoice;
        }
        
        private static bool InvoiceAlreadyExist(IReadOnlyList<Invoice> invoices, string number)
        {
            var exist = invoices.Any(x => x.Number.Equals(number, StringComparison.OrdinalIgnoreCase));

            return exist;
        }
    }
    
}