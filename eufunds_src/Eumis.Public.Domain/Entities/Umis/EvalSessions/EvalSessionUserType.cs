using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionUserType
    {
        [Description("Администратор на сесия")]
        Administrator = 1,

        [Description("Оценител")]
        Assessor = 2,

        [Description("Помощник оценител")]
        AssistantAssessor = 3,

        [Description("Наблюдател")]
        Observer = 4
    }
}