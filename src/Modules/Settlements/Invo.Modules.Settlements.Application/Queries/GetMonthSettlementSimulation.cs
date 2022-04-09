using System;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Shared.Abstractions.Queries;

namespace Invo.Modules.Settlements.Application.Queries
{
    public class GetMonthSettlementSimulation : IQuery<MonthSettlement>
    {
        public Guid CompanyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}