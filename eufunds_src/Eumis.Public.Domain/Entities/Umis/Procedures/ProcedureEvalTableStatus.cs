using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public enum ProcedureEvalTableStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Приключена")]
        Ended = 2,
    }
}
