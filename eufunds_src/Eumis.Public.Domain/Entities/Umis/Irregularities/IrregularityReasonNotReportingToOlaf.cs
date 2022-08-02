using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum IrregularityReasonNotReportingToOlaf
    {
        [Description("Под прага за докладване до ОЛАФ")]
        NotNecessary = 1,

        [Description("Попада в изключенията за докладване до ОЛАФ")]
        Exception = 2
    }
}
