using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts
{
    public enum DebtReimbursedAmountCreationType
    {
        [Description(Description = nameof(DomainEnumTexts.DebtReimbursedAmountCreationType_Manual), ResourceType = typeof(DomainEnumTexts))]
        Manual = 1,

        [Description(Description = nameof(DomainEnumTexts.DebtReimbursedAmountCreationType_SAPImport), ResourceType = typeof(DomainEnumTexts))]
        SAPImport = 2,
    }
}