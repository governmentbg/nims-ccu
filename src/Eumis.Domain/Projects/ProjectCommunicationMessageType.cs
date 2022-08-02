using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectCommunicationMessageType
    {
        // not accepted in db, so it must be changed before inserting
        None = 0,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationMessageType_Question), ResourceType = typeof(DomainEnumTexts))]
        Question = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationMessageType_Answer), ResourceType = typeof(DomainEnumTexts))]
        Answer = 2,
    }
}
