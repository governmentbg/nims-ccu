using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.CompensationDocuments
{
    public enum CompensationDocumentType
    {
        [Description("Договорени")]
        Contracted = 1,

        [Description("Поискани")]
        Requested = 2,

        [Description("Реално изплатени суми")]
        ActuallyPaidAmount = 3
    }
}
