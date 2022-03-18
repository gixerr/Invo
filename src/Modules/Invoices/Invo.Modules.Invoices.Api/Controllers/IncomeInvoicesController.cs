using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.DTO;
using Invo.Modules.Invoices.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invo.Modules.Invoices.Api.Controllers
{
    internal class IncomeInvoicesController : BaseController
    {
        private readonly IIncomeInvoiceService _incomeInvoiceService;

        public IncomeInvoicesController(IIncomeInvoiceService incomeInvoiceService)
        {
            _incomeInvoiceService = incomeInvoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InvoiceGetDto>>> BrowseAsync()
            => Ok(await _incomeInvoiceService.BrowseAsync());

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<InvoiceDetailsDto>> Get(Guid id) 
            => OkOrNotFound(await _incomeInvoiceService.GetAsync(id));

        [HttpGet("seller/{id:guid}")]
        public async Task<ActionResult<InvoiceGetDto>> GetBySellerIdAsync(Guid id)
            => Ok(await _incomeInvoiceService.BrowseBySellerAsync(id));

        [HttpPost]
        public async Task<ActionResult> AddAsync(InvoiceAddDto dto)
        {
            await _incomeInvoiceService.AddAsync(dto);

            return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, InvoiceUpdateDto dto)
        {
            dto.Id = id;
            await _incomeInvoiceService.UpdateAsync(dto);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _incomeInvoiceService.DeleteAsync(id);

            return NoContent();
        }
    }
}