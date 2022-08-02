using Eumis.Common.Json;

namespace Eumis.Domain.RequestPackages
{
    public enum RequestStatus
    {
        [Description(Description = nameof(DomainEnumTexts.RequestStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.RequestStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.RequestStatus_Checked), ResourceType = typeof(DomainEnumTexts))]
        Checked = 3,

        [Description(Description = nameof(DomainEnumTexts.RequestStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 4,

        [Description(Description = nameof(DomainEnumTexts.RequestStatus_Rejected), ResourceType = typeof(DomainEnumTexts))]
        Rejected = 5,

        [Description(Description = nameof(DomainEnumTexts.RequestStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 6,
    }
}
