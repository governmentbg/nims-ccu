namespace Eumis.Data.FlatFinancialCorrections.ViewObjects
{
    public class FlatFinancialCorrectionItemVO
    {
        public int FlatFinancialCorrectionLevelItemId { get; set; }

        public int FlatFinancialCorrectionId { get; set; }

        public int ItemId { get; set; }

        public decimal? Percent { get; set; }

        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? TotalAmount { get; set; }
    }
}
