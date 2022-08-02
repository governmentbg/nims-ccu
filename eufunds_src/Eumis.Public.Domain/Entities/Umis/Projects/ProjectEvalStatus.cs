using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public enum ProjectEvalStatus
    {
        [Description("Оценяване")]
        Evaluation = 1,

        [Description("Приключила оценка")]
        Evaluated = 2,

        [Description("Договориран")]
        Contracted = 3
    }
}
