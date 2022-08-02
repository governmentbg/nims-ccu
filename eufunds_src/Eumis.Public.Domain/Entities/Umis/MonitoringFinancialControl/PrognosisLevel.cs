using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public enum PrognosisLevel
    {
        [Description("Оперативна програма")]
        Programme = 1,

        [Description("Приоритетна ос")]
        ProgrammePriority = 2,

        [Description("Процедура")]
        Procedure = 3
    }
}