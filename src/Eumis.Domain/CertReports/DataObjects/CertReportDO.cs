using System;

namespace Eumis.Domain.CertReports.DataObjects
{
    public class CertReportDO
    {
        public CertReportDO()
        {
        }

        public CertReportDO(CertReport certReport)
        {
            this.CertReportId = certReport.CertReportId;
            this.ProgrammeId = certReport.ProgrammeId;
            this.OrderNum = certReport.OrderNum;
            this.OrderVersionNum = certReport.OrderVersionNum;
            this.RegDate = certReport.RegDate;
            this.DateFrom = certReport.DateFrom;
            this.DateTo = certReport.DateTo;
            this.Status = certReport.Status;
            this.StatusNote = certReport.StatusNote;
            this.Type = certReport.Type;
            this.ApprovalDate = certReport.ApprovalDate;
            this.CertReportNumber = certReport.CertReportNumber;

            this.CreateDate = certReport.CreateDate;
            this.ModifyDate = certReport.ModifyDate;
            this.Version = certReport.Version;
        }

        public int? CertReportId { get; set; }

        public int? ProgrammeId { get; set; }

        public int? OrderNum { get; set; }

        public int? OrderVersionNum { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public CertReportStatus Status { get; set; }

        public string StatusNote { get; set; }

        public CertReportType? Type { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CertReportNumber { get; set; }

        public byte[] Version { get; set; }
    }
}
