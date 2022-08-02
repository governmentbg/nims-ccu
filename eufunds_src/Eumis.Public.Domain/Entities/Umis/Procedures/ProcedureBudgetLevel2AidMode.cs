using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public enum ProcedureBudgetLevel2AidMode
    {
        [Description("de minimis")]
        Deminimis = 1,

        [Description("Държавна помощ")]
        StateAid = 2,

        [Description("Неприложимо")]
        NotApplicable = 3,
    }
}
