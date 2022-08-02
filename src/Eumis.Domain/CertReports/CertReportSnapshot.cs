using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.CertReports
{
    public partial class CertReportSnapshot : IAggregateRoot
    {
        public CertReportSnapshot()
        {
            this.CertReportSnapshotFiles = new List<CertReportSnapshotFile>();
        }

        public CertReportSnapshot(
            int certReportId,
            CertReportSnapshotJson certReportSnapshotJson,
            decimal? approvedEuAmount,
            decimal? approvedBgAmount,
            decimal? approvedBfpTotalAmount,
            decimal? approvedSelfAmount,
            decimal? approvedTotalAmount,
            decimal? certifiedEuAmount,
            decimal? certifiedBgAmount,
            decimal? certifiedBfpTotalAmount,
            decimal? certifiedSelfAmount,
            decimal? certifiedTotalAmount)
        {
            this.CertReportId = certReportId;

            this.CertReportText = JsonConvert.SerializeObject(certReportSnapshotJson, Formatting.None);

            this.ApprovedEuAmount = approvedEuAmount;
            this.ApprovedBgAmount = approvedBgAmount;
            this.ApprovedBfpTotalAmount = approvedBfpTotalAmount;
            this.ApprovedSelfAmount = approvedSelfAmount;
            this.ApprovedTotalAmount = approvedTotalAmount;
            this.CertifiedEuAmount = certifiedEuAmount;
            this.CertifiedBgAmount = certifiedBgAmount;
            this.CertifiedBfpTotalAmount = certifiedBfpTotalAmount;
            this.CertifiedSelfAmount = certifiedSelfAmount;
            this.CertifiedTotalAmount = certifiedTotalAmount;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int CertReportSnapshotId { get; private set; }

        public int CertReportId { get; private set; }

        public string CertReportText { get; private set; }

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

        public virtual ICollection<CertReportSnapshotFile> CertReportSnapshotFiles { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CertReportSnapshotMap : EntityTypeConfiguration<CertReportSnapshot>
    {
        public CertReportSnapshotMap()
        {
            // Primary Key
            this.HasKey(t => t.CertReportSnapshotId);

            // Properties
            this.Property(t => t.CertReportSnapshotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CertReportId)
                .IsRequired();

            this.Property(t => t.CertReportText)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CertReportSnapshots");
            this.Property(t => t.CertReportSnapshotId).HasColumnName("CertReportSnapshotId");
            this.Property(t => t.CertReportId).HasColumnName("CertReportId");
            this.Property(t => t.CertReportText).HasColumnName("CertReportText");

            this.Property(t => t.ApprovedEuAmount).HasColumnName("ApprovedEuAmount");
            this.Property(t => t.ApprovedBgAmount).HasColumnName("ApprovedBgAmount");
            this.Property(t => t.ApprovedBfpTotalAmount).HasColumnName("ApprovedBfpTotalAmount");
            this.Property(t => t.ApprovedSelfAmount).HasColumnName("ApprovedSelfAmount");
            this.Property(t => t.ApprovedTotalAmount).HasColumnName("ApprovedTotalAmount");
            this.Property(t => t.CertifiedEuAmount).HasColumnName("CertifiedEuAmount");
            this.Property(t => t.CertifiedBgAmount).HasColumnName("CertifiedBgAmount");
            this.Property(t => t.CertifiedBfpTotalAmount).HasColumnName("CertifiedBfpTotalAmount");
            this.Property(t => t.CertifiedSelfAmount).HasColumnName("CertifiedSelfAmount");
            this.Property(t => t.CertifiedTotalAmount).HasColumnName("CertifiedTotalAmount");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
