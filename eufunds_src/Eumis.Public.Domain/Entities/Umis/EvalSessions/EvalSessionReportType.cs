using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionReportType
    {
        [Description("Протокол")]
        Protocol = 1,

        [Description("Доклад")]
        Report = 2,

        [Description("Решение")]
        Decision = 3
    }
}
