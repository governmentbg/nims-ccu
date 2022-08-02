using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class ApprovedAmountsCorrectionsVO
    {
        public ApprovedAmountsCorrectionsVO()
        {
            this.ApprovedEuAmount = 0;
            this.ApprovedBgAmount = 0;
            this.ApprovedSelfAmount = 0;
            this.ApprovedBgEuAmount = 0;
            this.ReimbursedBgEuAmount = 0;
        }

        public int ContractReportFinancialCorrectionId { get; set; }

        public string ProgrammePriorityName { get; set; }

        public int CertNum { get; set; }

        public DateTime? CertDate { get; set; }

        public string ContractNum { get; set; }

        public int ReportNum { get; set; }

        public DateTime? ReportDate { get; set; }

        public int CorrectionNum { get; set; }

        public DateTime? CorrectionDate { get; set; }

        public string CorrectionNote { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? ApprovedBgEuAmount { get; set; }

        public decimal? ReimbursedBgEuAmount { get; set; }
    }
}
