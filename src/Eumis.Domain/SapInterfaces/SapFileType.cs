using Eumis.Common.Json;

namespace Eumis.Domain.SapInterfaces
{
    public enum SapFileType
    {
        [Description(Description = nameof(DomainEnumTexts.SapFileType_PaidAmounts), ResourceType = typeof(DomainEnumTexts))]
        PaidAmounts = 1,

        [Description(Description = nameof(DomainEnumTexts.SapFileType_DistributedLimits), ResourceType = typeof(DomainEnumTexts))]
        DistributedLimits = 2,
    }
}
