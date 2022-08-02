using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Prognoses.ViewObjects
{
    public class PrognosisMonthlyReportItemVO
    {
        public Month Month { get; set; }

        public decimal? AdvancePaymentAmount { get; set; }

        public decimal? AdvanceVerPaymentAmount { get; set; }

        public decimal? IntermediatePaymentAmount { get; set; }

        public decimal? FinalPaymentAmount { get; set; }
    }
}
