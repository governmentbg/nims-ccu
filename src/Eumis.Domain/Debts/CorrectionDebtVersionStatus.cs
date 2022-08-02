using Eumis.Common.Json;

namespace Eumis.Domain.Debts
{
    public enum CorrectionDebtVersionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.CorrectionDebtVersionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.CorrectionDebtVersionStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 2,

        [Description(Description = nameof(DomainEnumTexts.CorrectionDebtVersionStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}
