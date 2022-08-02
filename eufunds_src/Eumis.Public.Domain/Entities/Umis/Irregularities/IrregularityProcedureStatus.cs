using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum IrregularityProcedureStatus
    {
        [Description("Административни действия")]
        AP = 1,

        [Description("Съдебно производство")]
        JP = 2,

        [Description("Наказателно производство")]
        PP = 3,

        [Description("Приключило производство")]
        PX = 4
    }
}
