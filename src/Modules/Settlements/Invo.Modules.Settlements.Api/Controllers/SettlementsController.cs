using System.Threading.Tasks;
using Invo.Modules.Settlements.Application.Commands;
using Invo.Modules.Settlements.Application.Queries;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Shared.Abstractions.Commands;
using Invo.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Invo.Modules.Settlements.Api.Controllers
{
    internal class SettlementsController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public SettlementsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("month/simulation")]
        public async Task<ActionResult<MonthSettlement>> GetSimulationAsync(
            [FromQuery] GetMonthSettlementSimulation query) 
            => OkOrNotFound(await _queryDispatcher.QueryAsync(query));

        [HttpPost("month/create")]
        public async Task<ActionResult> CreateMonthSettlement(CreateMonthSettlement command)
        {
            await _commandDispatcher.SendAsync(command);
            return Created(string.Empty, null);
        }
    }
}