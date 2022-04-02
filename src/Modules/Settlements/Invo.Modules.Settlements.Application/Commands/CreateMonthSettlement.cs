using System;
using Invo.Shared.Abstractions.Commands;

namespace Invo.Modules.Settlements.Application.Commands
{
    public class CreateMonthSettlement : ICommand
    {
        public Guid CompanyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}