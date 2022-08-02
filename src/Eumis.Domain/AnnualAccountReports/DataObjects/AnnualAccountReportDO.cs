using Eumis.Domain.AnnualAccountReports;
using System;

namespace Eumis.Domain.AnnualAccountReports.DataObjects
{
    public class AnnualAccountReportDO
    {
        public AnnualAccountReportDO()
        {
        }

        public AnnualAccountReportDO(AnnualAccountReport accountReport)
        {
            this.AnnualAccountReportId = accountReport.AnnualAccountReportId;
            this.ProgrammeId = accountReport.ProgrammeId;
            this.OrderNum = accountReport.OrderNum;
            this.OrderVersionNum = accountReport.OrderVersionNum;
            this.RegDate = accountReport.RegDate;
            this.AccountPeriod = accountReport.AccountPeriod;
            this.Status = accountReport.Status;
            this.StatusNote = accountReport.StatusNote;
            this.ApprovalDate = accountReport.ApprovalDate;

            this.CreateDate = accountReport.CreateDate;
            this.ModifyDate = accountReport.ModifyDate;
            this.Version = accountReport.Version;
        }

        public int? AnnualAccountReportId { get; set; }

        public int? ProgrammeId { get; set; }

        public int? OrderNum { get; set; }

        public int? OrderVersionNum { get; set; }

        public DateTime? RegDate { get; set; }

        public AnnualAccountReportPeriod AccountPeriod { get; set; }

        public AnnualAccountReportStatus Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
