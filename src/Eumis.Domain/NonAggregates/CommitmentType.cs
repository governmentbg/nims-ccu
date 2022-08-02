using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum CommitmentType
    {
        [Description(Description = nameof(DomainEnumTexts.CommitmentType_CivilContract), ResourceType = typeof(DomainEnumTexts))]
        CivilContract = 1,

        [Description(Description = nameof(DomainEnumTexts.CommitmentType_EmploymentContract), ResourceType = typeof(DomainEnumTexts))]
        EmploymentContract = 2,

        [Description(Description = nameof(DomainEnumTexts.CommitmentType_Order), ResourceType = typeof(DomainEnumTexts))]
        Order = 3,

        [Description(Description = nameof(DomainEnumTexts.CommitmentType_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 4,
    }
}