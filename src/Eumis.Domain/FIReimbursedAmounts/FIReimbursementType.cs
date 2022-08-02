using Eumis.Common.Json;

namespace Eumis.Domain.FIReimbursedAmounts
{
    public enum FIReimbursementType
    {
        [Description(Description = nameof(DomainEnumTexts.FIReimbursementType_ExGratia), ResourceType = typeof(DomainEnumTexts))]
        ExGratia = 1,

        [Description(Description = nameof(DomainEnumTexts.FIReimbursementType_NAP), ResourceType = typeof(DomainEnumTexts))]
        NAP = 2,

        [Description(Description = nameof(DomainEnumTexts.FIReimbursementType_Collateral), ResourceType = typeof(DomainEnumTexts))]
        Collateral = 3,
    }
}