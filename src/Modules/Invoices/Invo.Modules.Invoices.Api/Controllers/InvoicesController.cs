using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.DTO;
using Invo.Modules.Invoices.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invo.Modules.Invoices.Api.Controllers
{
    internal class InvoicesController : BaseController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InvoiceGetDto>>> BrowseAsync()
            => Ok(await _invoiceService.BrowseAsync());

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<InvoiceDetailsUpdateDto>> Get(Guid id) 
            => OkOrNotFound(await _invoiceService.GetAsync(id));

        [HttpGet("seller/{id:guid}")]
        public async Task<ActionResult<InvoiceGetDto>> GetBySellerIdAsync(Guid id)
            => Ok(await _invoiceService.BrowseBySellerAsync(id));

        [HttpPost]
        public async Task<ActionResult> AddAsync(InvoiceAddUpdateDto dto)
        {
            await _invoiceService.AddAsync(dto);

            return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, InvoiceAddUpdateDto dto)
        {
            dto.Id = id;
            await _invoiceService.UpdateAsync(dto);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _invoiceService.DeleteAsync(id);

            return NoContent();
        }
    }
}