using System;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Domain.Models.Payments
{
    public class SocialSecurity : ICalculation
    {
        public decimal HealthCareContribution => (decimal)0.049;
        public decimal SocialSecurityContribution => 1100;
        public decimal Value { get; }
        
        public SocialSecurity(MonthIncome monthIncome, TaxToPay taxToPay)
        {
            Value = Math.Round(GetHealthcareContributionAmount(monthIncome, taxToPay) + SocialSecurityContribution, 2);
        }

        private decimal GetHealthcareContributionAmount(MonthIncome monthIncome, TaxToPay taxToPay)
            => (monthIncome.Value - taxToPay.Value) * HealthCareContribution;
    }
}