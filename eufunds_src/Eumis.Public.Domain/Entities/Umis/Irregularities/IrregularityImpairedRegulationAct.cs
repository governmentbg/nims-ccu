using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum IrregularityImpairedRegulationAct
    {
        [Description("Решение")]
        Dec = 1,

        [Description("Директива")]
        Dir = 2,

        [Description("Регламент")]
        Reg = 3,

        [Description("Споразумение")]
        Agr = 4
    }
}
