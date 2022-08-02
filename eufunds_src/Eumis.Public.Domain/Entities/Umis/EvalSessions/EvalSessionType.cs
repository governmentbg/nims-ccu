using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionType
    {
        [Description("За предварителен подбор")]
        Preselection = 1,

        [Description("За оценка на проектни предложения")]
        ProjectEvaluation = 2,

        [Description("За оценка на проектни фишове")]
        ProjectFicheEvaluation = 3,
    }
}
