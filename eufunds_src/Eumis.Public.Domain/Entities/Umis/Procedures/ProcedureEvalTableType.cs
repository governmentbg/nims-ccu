using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public enum ProcedureEvalTableType
    {
        [Description("Оценка на административното съответствие и допустимостта")]
        AdminAdmiss = 1,

        [Description("Техническа и финансова оценка")]
        TechFinance = 2,

        [Description("Комплексна оценка")]
        Complex = 3,

        [Description("Предварителна оценка")]
        Preliminary = 4,
    }
}
