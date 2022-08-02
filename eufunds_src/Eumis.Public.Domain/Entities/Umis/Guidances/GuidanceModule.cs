using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Guidances
{
    public enum GuidanceModule
    {
        [Description("Вътрешна система")]
        InternalSystem = 1,

        [Description("Портал за електронно кандидатстване")]
        ApplicationsPortal = 2,

        [Description("Портал за електронно отчитане")]
        ReportsPortal = 3,

        [Description("Публичен портал")]
        PublicPortal = 4
    }
}
