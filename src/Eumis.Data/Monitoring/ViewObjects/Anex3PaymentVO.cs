using System;
using Eumis.Domain.Audits;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class Anex3PaymentVO
    {
        public DateTime? SubmitDate { get; set; }

        public DateTime? CheckedDate { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? PaidBfpTotalAmount { get; set; }

        public decimal? AdditionalIncome { get; set; }

        public DateTime? SpotCheckDateFrom { get; set; }

        public string SpotCheckTeam { get; set; }

        public DateTime? AuditStartDate { get; set; }

        public AuditInstitution? AuditInstitution { get; set; }
    }
}
