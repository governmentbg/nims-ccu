using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Prognoses.ViewObjects
{
    public class PrognosisYearlyReportItemVO
    {
        public Year Year { get; set; }

        public Quarter Quarter { get; set; }

        public decimal? AdvancePaymentAmount { get; set; }

        public decimal? AdvanceVerPaymentAmount { get; set; }

        public decimal? IntermediatePaymentAmount { get; set; }

        public decimal? FinalPaymentAmount { get; set; }
    }
}
