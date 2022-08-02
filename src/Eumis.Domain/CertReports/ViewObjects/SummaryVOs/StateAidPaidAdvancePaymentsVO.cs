using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class StateAidPaidAdvancePaymentsVO
    {
        public StateAidPaidAdvancePaymentsVO()
        {
            this.ApprovedTotalAmount = 0;
            this.ApprovedAdvancedTotalAmountCSD = 0;
            this.UncoveredAmountByBeneficient = 0;
        }

        public string ProgrammePriorityName { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? ApprovedAdvancedTotalAmountCSD { get; set; }

        public decimal? UncoveredAmountByBeneficient { get; set; }
    }
}
