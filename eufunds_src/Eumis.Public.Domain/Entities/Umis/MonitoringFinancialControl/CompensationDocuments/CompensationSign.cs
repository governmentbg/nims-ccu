using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.CompensationDocuments
{
    public enum CompensationSign
    {
        [Description("+")]
        Plus = 1,

        [Description("-")]
        Minus = -1,
    }
}
