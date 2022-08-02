using System;

namespace Eumis.Domain.CertReports.DataObjects
{
    public class CertReportSnapshotDO
    {
        public CertReportSnapshotDO()
        {
        }

        public CertReportSnapshotDO(CertReportSnapshot certReportSnapshot)
        {
            this.CertReportSnapshotId = certReportSnapshot.CertReportId;
            this.CertReportId = certReportSnapshot.CertReportId;
            this.CertReportText = certReportSnapshot.CertReportText;

            this.ApprovedEuAmount = certReportSnapshot.ApprovedEuAmount;
            this.ApprovedBgAmount = certReportSnapshot.ApprovedBgAmount;
            this.ApprovedBfpTotalAmount = certReportSnapshot.ApprovedBfpTotalAmount;
            this.ApprovedSelfAmount = certReportSnapshot.ApprovedSelfAmount;
            this.ApprovedTotalAmount = certReportSnapshot.ApprovedTotalAmount;
            this.CertifiedEuAmount = certReportSnapshot.CertifiedEuAmount;
            this.CertifiedBgAmount = certReportSnapshot.CertifiedBgAmount;
            this.CertifiedBfpTotalAmount = certReportSnapshot.CertifiedBfpTotalAmount;
            this.CertifiedSelfAmount = certReportSnapshot.CertifiedSelfAmount;
            this.CertifiedTotalAmount = certReportSnapshot.CertifiedTotalAmount;

            this.CreateDate = certReportSnapshot.CreateDate;
            this.ModifyDate = certReportSnapshot.ModifyDate;
            this.Version = certReportSnapshot.Version;
        }

        public int CertReportSnapshotId { get; private set; }

        public int CertReportId { get; private set; }

        public string CertReportText { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? CertifiedEuAmount { get; set; }

        public decimal? CertifiedBgAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public decimal? CertifiedTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
