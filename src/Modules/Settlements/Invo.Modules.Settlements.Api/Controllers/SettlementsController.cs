using System;
using System.Threading.Tasks;
using Invo.Modules.Settlements.Application.Commands;
using Invo.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Invo.Modules.Settlements.Api.Controllers
{
    internal class SettlementsController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SettlementsController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        
        [HttpPost("month/create")]
        public async Task<ActionResult> CreateMonthSettlement(CreateMonthSettlement command)
        {
            await _commandDispatcher.SendAsync(command);
            return Created(string.Empty, null);
        }
    }
}