using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class EligibleProgrammePriorityExpensesVO
    {
        public EligibleProgrammePriorityExpensesVO()
        {
            this.CurrentApprovedEuAmount = 0;
            this.CurrentApprovedBgAmount = 0;
            this.CurrentApprovedSelfAmount = 0;
            this.CurrentApprovedBgEuAmount = 0;
            this.CurrentApprovedBgEuSelfAmount = 0;

            this.OtherApprovedEuAmount = 0;
            this.OtherApprovedBgAmount = 0;
            this.OtherApprovedSelfAmount = 0;
            this.OtherApprovedBgEuAmount = 0;
            this.OtherApprovedBgEuSelfAmount = 0;
        }

        public string ProgrammePriorityName { get; set; }

        public decimal CurrentApprovedEuAmount { get; set; }

        public decimal CurrentApprovedBgAmount { get; set; }

        public decimal CurrentApprovedSelfAmount { get; set; }

        public decimal CurrentApprovedBgEuAmount { get; set; }

        public decimal CurrentApprovedBgEuSelfAmount { get; set; }

        public decimal OtherApprovedEuAmount { get; set; }

        public decimal OtherApprovedBgAmount { get; set; }

        public decimal OtherApprovedSelfAmount { get; set; }

        public decimal OtherApprovedBgEuAmount { get; set; }

        public decimal OtherApprovedBgEuSelfAmount { get; set; }
    }
}
