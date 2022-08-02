using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum IrregularitySanctionProcedureKind
    {
        [Description("Административни")]
        Adm = 1,

        [Description("Наказателни")]
        Pen = 2,

        [Description("Административни и наказателни")]
        Pxx = 3
    }
}
