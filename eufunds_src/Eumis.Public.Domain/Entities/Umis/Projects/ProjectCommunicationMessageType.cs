using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public enum ProjectCommunicationMessageType
    {
        [Description("Въпрос")]
        Question = 1,

        [Description("Отговор")]
        Answer = 2
    }
}
