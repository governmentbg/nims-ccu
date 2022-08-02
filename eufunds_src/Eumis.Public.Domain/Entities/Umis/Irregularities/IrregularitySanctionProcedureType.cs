using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum IrregularitySanctionProcedureType
    {
        [Description("Не е взето решение")]
        SP1 = 1,

        [Description("Няма да се налага санкция")]
        SP2 = 2,

        [Description("Предстои определянето на санкция")]
        SP3 = 3,

        [Description("Наложена санкция")]
        SP4 = 4,
    }
}
