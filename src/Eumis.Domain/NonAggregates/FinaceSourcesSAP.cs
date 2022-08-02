using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum FinaceSourcesSAP
    {
        [Description(Description = nameof(DomainEnumTexts.FinaceSourcesSAP_Bfp), ResourceType = typeof(DomainEnumTexts))]
        Bfp = 1,

        [Description(Description = nameof(DomainEnumTexts.FinaceSourcesSAP_SelfFinancing), ResourceType = typeof(DomainEnumTexts))]
        SelfFinancing = 2,

        [Description(Description = nameof(DomainEnumTexts.FinaceSourcesSAP_AdvanceVerification), ResourceType = typeof(DomainEnumTexts))]
        AdvanceVerification = 3,
    }
}
