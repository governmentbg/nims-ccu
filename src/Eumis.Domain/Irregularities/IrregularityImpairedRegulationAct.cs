using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularityImpairedRegulationAct
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularityImpairedRegulationAct_Dec), ResourceType = typeof(DomainEnumTexts))]
        Dec = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularityImpairedRegulationAct_Dir), ResourceType = typeof(DomainEnumTexts))]
        Dir = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularityImpairedRegulationAct_Reg), ResourceType = typeof(DomainEnumTexts))]
        Reg = 3,

        [Description(Description = nameof(DomainEnumTexts.IrregularityImpairedRegulationAct_Agr), ResourceType = typeof(DomainEnumTexts))]
        Agr = 4,
    }
}
