namespace Eumis.Public.Web.Models.OPProfile
{
    public class ProgrammeBudgetBySource
    {
        public ProgrammeBudgetBySource(
            string year,
            decimal europeanRegionalDevelopmentFund,
            decimal europeanSocialFund,
            decimal cohesionFund,
            decimal bgAmount,
            decimal youthEmploymentInitiative,
            decimal fundForEuropeanAidToTheMostDeprived,
            decimal efmdr,
            decimal ezfrsr,
            decimal fvs,
            decimal fumi,
            decimal other,
            decimal eeafm,
            decimal nfm)
        {
            this.Year = year;
            this.EuropeanRegionalDevelopmentFund = europeanRegionalDevelopmentFund;
            this.EuropeanSocialFund = europeanSocialFund;
            this.CohesionFund = cohesionFund;
            this.BgAmount = bgAmount;
            this.YouthEmploymentInitiative = youthEmploymentInitiative;
            this.FundForEuropeanAidToTheMostDeprived = fundForEuropeanAidToTheMostDeprived;
            this.EFMDR = efmdr;
            this.EZFRSR = ezfrsr;
            this.FVS = fvs;
            this.FUMI = fumi;
            this.Other = other;
            this.EEAFM = eeafm;
            this.NFM = nfm;
        }

        public string Year { get; set; }

        public decimal EuropeanRegionalDevelopmentFund { get; set; }

        public decimal EuropeanSocialFund { get; set; }

        public decimal CohesionFund { get; set; }

        public decimal BgAmount { get; set; }

        public decimal YouthEmploymentInitiative { get; set; }

        public decimal FundForEuropeanAidToTheMostDeprived { get; set; }

        public decimal EFMDR { get; set; }

        public decimal EZFRSR { get; set; }

        public decimal FVS { get; set; }

        public decimal FUMI { get; set; }

        public decimal Other { get; set; }

        public decimal EEAFM { get; set; }

        public decimal NFM { get; set; }

        public decimal YearTotal
        {
            get
            {
                return this.EuropeanRegionalDevelopmentFund + this.EuropeanSocialFund + this.CohesionFund + this.BgAmount + this.YouthEmploymentInitiative + this.FundForEuropeanAidToTheMostDeprived
                    + this.EFMDR + this.EZFRSR + this.FVS + this.FUMI + this.Other + this.EEAFM + this.NFM;
            }
        }
    }
}