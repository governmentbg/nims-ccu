namespace Eumis.Public.Data.UmisVOs
{
    public class ContractedFundsByYearAndSourceVO
    {
        public string Year { get; set; }

        public decimal CohesionFund { get; set; }

        public decimal EuropeanRegionalDevelopmentFund { get; set; }

        public decimal EuropeanSocialFund { get; set; }

        public decimal FundForEuropeanAidToTheMostDeprived { get; set; }

        public decimal YouthEmploymentInitiative { get; set; }

        public decimal EFMDR { get; set; }

        public decimal EZFRSR { get; set; }

        public decimal FVS { get; set; }

        public decimal FUMI { get; set; }

        public decimal Other { get; set; }

        public decimal BgAmount { get; set; }

        public decimal SelfAmount { get; set; }

        public decimal YearTotal
        {
            get
            {
                return this.CohesionFund + this.EuropeanRegionalDevelopmentFund + this.EuropeanSocialFund + this.FundForEuropeanAidToTheMostDeprived +
                       this.YouthEmploymentInitiative + this.EFMDR + this.EZFRSR + this.FVS + this.FUMI + this.Other + this.BgAmount + this.SelfAmount;
            }
        }
    }
}
