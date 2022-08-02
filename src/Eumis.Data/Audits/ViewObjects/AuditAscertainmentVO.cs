namespace Eumis.Data.Audits.ViewObjects
{
    public class AuditAscertainmentVO
    {
        public int AscertainmentId { get; set; }

        public int OrderNum { get; set; }

        public int AuditId { get; set; }

        public string Ascertainment { get; set; }

        public string Recommendation { get; set; }

        public bool IsFinancial { get; set; }

        public decimal? FinancialSum { get; set; }
    }
}
