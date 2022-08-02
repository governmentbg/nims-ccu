using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum IrregularityClassification
    {
        [Description("Установена липса на нередност")]
        NoIrregularity = 1,

        [Description("Нередност")]
        Irregularity = 2,

        [Description("Подозрение за измама")]
        FraudSuspicion = 3,

        [Description("Установена измама")]
        Fraud = 4
    }
}
