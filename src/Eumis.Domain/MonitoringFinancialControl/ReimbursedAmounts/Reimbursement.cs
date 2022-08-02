using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts
{
    public enum Reimbursement
    {
        [Description(Description = nameof(DomainEnumTexts.Reimbursement_Bank), ResourceType = typeof(DomainEnumTexts))]
        Bank = 1,

        [Description(Description = nameof(DomainEnumTexts.Reimbursement_Deduction), ResourceType = typeof(DomainEnumTexts))]
        Deduction = 2,

        [Description(Description = nameof(DomainEnumTexts.Reimbursement_WrittenOff), ResourceType = typeof(DomainEnumTexts))]
        WrittenOff = 3,

        [Description(Description = nameof(DomainEnumTexts.Reimbursement_DistributedLimitDeduction), ResourceType = typeof(DomainEnumTexts))]
        DistributedLimitDeduction = 4,
    }
}
