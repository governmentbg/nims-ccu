using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System.Collections.Generic;

namespace Eumis.Public.Data.UmisVOs
{
    public class ProgrammeBudgetBySourceVO
    {
        public decimal CohesionFundTotal { get; set; }

        public decimal EuropeanRegionalDevelopmentFundTotal { get; set; }

        public decimal EuropeanSocialFundTotal { get; set; }

        public decimal FundForEuropeanAidToTheMostDeprivedTotal { get; set; }

        public decimal YouthEmploymentInitiativeTotal { get; set; }

        public decimal EFMDRTotal { get; set; }

        public decimal EZFRSRTotal { get; set; }

        public decimal FVSTotal { get; set; }

        public decimal FUMITotal { get; set; }

        public decimal OtherTotal { get; set; }

        public decimal EEAFMTotal { get; set; }

        public decimal NFMTotal { get; set; }

        public decimal BgAmountTotal { get; set; }

        public IList<ProgrammeBudgetBySourceChildVO> Items { get; set; }

        public IList<FinanceSource> Sources { get; set; }
    }
}
