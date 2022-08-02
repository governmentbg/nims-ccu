using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public enum ProjectCommunicationStatus
    {
        [Description("Въпрос в чернова")]
        DraftQuestion = 1,

        [Description("Изпратен въпрос")]
        Question = 2,

        [Description("Отговор чернова")]
        DraftAnswer = 3,

        [Description("Отговор финализиран")]
        AnswerFinalized = 4,

        [Description("Изпратен отговор")]
        Answer = 5,

        [Description("Изпратен отговор на хартия")]
        PaperAnswer = 6,

        [Description("Приет отговор")]
        Applied = 7,

        [Description("Отхвърлен отговор")]
        Rejected = 8,

        [Description("Анулиран")]
        Canceled = 9
    }
}
