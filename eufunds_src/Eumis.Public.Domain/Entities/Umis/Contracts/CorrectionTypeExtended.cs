using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum CorrectionTypeExtended
    {
        [Description("Финансова корекция")]
        FinancialCorrection = 1,

        [Description("Нередност")]
        Irregularity = 2,

        [Description("Финансова корекция и нередност")]
        FinancialCorrectionIrregularity = 3,

        [Description("Финансова корекция за системни пропуски")]
        FlatFinancialCorrection = 4
    }
}
