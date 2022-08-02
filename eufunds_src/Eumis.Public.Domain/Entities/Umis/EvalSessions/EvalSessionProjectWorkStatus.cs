using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionProjectWorkStatus
    {
        [Description("Комуникация")]
        ComunicationInProgress = 1,

        [Description("Редакция на проект")]
        DraftVersion = 2
    }
}