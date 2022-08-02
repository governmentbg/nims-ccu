using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public enum FinanceSource
    {
        [LocalizableDescriptionAttribute("FinanceSource_EuropeanSocialFund")]
        EuropeanSocialFund = 1,

        [LocalizableDescriptionAttribute("FinanceSource_EuropeanRegionalDevelopmentFund")]
        EuropeanRegionalDevelopmentFund = 2,

        [LocalizableDescriptionAttribute("FinanceSource_CohesionFund")]
        CohesionFund = 3,

        [LocalizableDescriptionAttribute("FinanceSource_YouthEmploymentInitiative")]
        YouthEmploymentInitiative = 4,

        [LocalizableDescriptionAttribute("FinanceSource_FundForEuropeanAidToTheMostDeprived")]
        FundForEuropeanAidToTheMostDeprived = 5,

        [LocalizableDescriptionAttribute("FinanceSource_EFMDR")]
        EFMDR = 6,

        [LocalizableDescriptionAttribute("FinanceSource_EZFRSR")]
        EZFRSR = 7,

        [LocalizableDescriptionAttribute("FinanceSource_FVS")]
        FVS = 8,

        [LocalizableDescriptionAttribute("FinanceSource_FUMI")]
        FUMI = 9,

        [LocalizableDescriptionAttribute("FinanceSource_Other")]
        Other = 10,

        [LocalizableDescriptionAttribute("FinanceSource_EEAFM")]
        EEAFM = 11,

        [LocalizableDescriptionAttribute("FinanceSource_NFM")]
        NFM = 12,
    }
}