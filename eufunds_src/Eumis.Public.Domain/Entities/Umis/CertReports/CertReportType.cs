using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertReports
{
    public enum CertReportType
    {
        [Description("Междинен")]
        Intermediate = 1,

        [Description("Финален")]
        Final = 2,

        [Description("Годишен")]
        Yearly = 3
    }
}