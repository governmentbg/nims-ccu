using Eumis.Common.Json;

namespace Eumis.Domain.OperationalMap.MapNodes
{
    public enum MapNodeStatus
    {
        [Description(Description = nameof(DomainEnumTexts.MapNodeStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.MapNodeStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.MapNodeStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 3,
    }
}
