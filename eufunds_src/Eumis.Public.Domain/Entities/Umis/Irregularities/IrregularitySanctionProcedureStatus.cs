using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum IrregularitySanctionProcedureStatus
    {
        [Description("Initiated")]
        Initiated = 1,

        [Description("Completed")]
        Completed = 2,

        [Description("Abandoned")]
        Abandoned = 3
    }
}
