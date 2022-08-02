using Eumis.Domain.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportTechnicalCorrectionIndicator : IAggregateRoot
    {
        private ContractReportTechnicalCorrectionIndicator()
        {
        }

        public ContractReportTechnicalCorrectionIndicator(
            int contractReportTechnicalCorrectionId,
            int contractReportIndicatorId,
            int contractReportTechnicalId,
            int contractReportId,
            int contractId,
            decimal? lastReportCorrectedCumulativeAmountMen,
            decimal? lastReportCorrectedCumulativeAmountWomen,
            decimal? lastReportCorrectedCumulativeAmountTotal)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportTechnicalCorrectionId = contractReportTechnicalCorrectionId;
            this.ContractReportIndicatorId = contractReportIndicatorId;
            this.ContractReportTechnicalId = contractReportTechnicalId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;

            this.Status = ContractReportTechnicalCorrectionIndicatorStatus.Draft;

            this.LastReportCorrectedCumulativeAmountMen = lastReportCorrectedCumulativeAmountMen;
            this.LastReportCorrectedCumulativeAmountWomen = lastReportCorrectedCumulativeAmountWomen;
            this.LastReportCorrectedCumulativeAmountTotal = lastReportCorrectedCumulativeAmountTotal ?? 0;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public ContractReportTechnicalCorrectionIndicator(
            int contractReportTechnicalCorrectionId,
            decimal? approvedCumulativeAmountMen,
            decimal? approvedCumulativeAmountWomen,
            decimal? approvedCumulativeAmountTotal,
            ContractReportTechnicalCorrectionIndicator copyFrom)
        {
            this.Gid = Guid.NewGuid();
            this.ContractReportTechnicalCorrectionId = contractReportTechnicalCorrectionId;

            this.Status = ContractReportTechnicalCorrectionIndicatorStatus.Draft;

            this.ContractReportIndicatorId = copyFrom.ContractReportIndicatorId;
            this.ContractReportTechnicalId = copyFrom.ContractReportTechnicalId;
            this.ContractReportId = copyFrom.ContractReportId;
            this.ContractId = copyFrom.ContractId;
            this.CheckedByUserId = null;
            this.CheckedDate = null;
            this.Notes = copyFrom.Notes;
            this.CorrectedApprovedPeriodAmountMen = copyFrom.CorrectedApprovedPeriodAmountMen;
            this.CorrectedApprovedPeriodAmountWomen = copyFrom.CorrectedApprovedPeriodAmountWomen;
            this.CorrectedApprovedPeriodAmountTotal = copyFrom.CorrectedApprovedPeriodAmountTotal;
            this.CorrectedApprovedCumulativeAmountMen = copyFrom.CorrectedApprovedCumulativeAmountMen;
            this.CorrectedApprovedCumulativeAmountWomen = copyFrom.CorrectedApprovedCumulativeAmountWomen;
            this.CorrectedApprovedCumulativeAmountTotal = copyFrom.CorrectedApprovedCumulativeAmountTotal;
            this.CorrectedApprovedResidueAmountMen = copyFrom.CorrectedApprovedResidueAmountMen;
            this.CorrectedApprovedResidueAmountWomen = copyFrom.CorrectedApprovedResidueAmountWomen;
            this.CorrectedApprovedResidueAmountTotal = copyFrom.CorrectedApprovedResidueAmountTotal;
            this.LastReportCorrectedCumulativeAmountMen = approvedCumulativeAmountMen;
            this.LastReportCorrectedCumulativeAmountWomen = approvedCumulativeAmountWomen;
            this.LastReportCorrectedCumulativeAmountTotal = approvedCumulativeAmountTotal ?? 0;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportTechnicalCorrectionIndicatorId { get; set; }

        public int ContractReportTechnicalCorrectionId { get; set; }

        public int ContractReportIndicatorId { get; set; }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public ContractReportTechnicalCorrectionIndicatorStatus Status { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public decimal? CorrectedApprovedPeriodAmountMen { get; set; }

        public decimal? CorrectedApprovedPeriodAmountWomen { get; set; }

        public decimal? CorrectedApprovedPeriodAmountTotal { get; set; }

        public decimal? CorrectedApprovedCumulativeAmountMen { get; set; }

        public decimal? CorrectedApprovedCumulativeAmountWomen { get; set; }

        public decimal? CorrectedApprovedCumulativeAmountTotal { get; set; }

        public decimal? CorrectedApprovedResidueAmountMen { get; set; }

        public decimal? CorrectedApprovedResidueAmountWomen { get; set; }

        public decimal? CorrectedApprovedResidueAmountTotal { get; set; }

        public decimal? LastReportCorrectedCumulativeAmountMen { get; set; }

        public decimal? LastReportCorrectedCumulativeAmountWomen { get; set; }

        public decimal LastReportCorrectedCumulativeAmountTotal { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportTechnicalCorrectionIndicatorMap : EntityTypeConfiguration<ContractReportTechnicalCorrectionIndicator>
    {
        public ContractReportTechnicalCorrectionIndicatorMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportTechnicalCorrectionIndicatorId);

            // Properties
            this.Property(t => t.ContractReportTechnicalCorrectionIndicatorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportTechnicalCorrectionId)
                .IsRequired();

            this.Property(t => t.ContractReportIndicatorId)
                .IsRequired();

            this.Property(t => t.ContractReportTechnicalId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.LastReportCorrectedCumulativeAmountTotal)
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
            this.ToTable("ContractReportTechnicalCorrectionIndicators");
            this.Property(t => t.ContractReportTechnicalCorrectionIndicatorId).HasColumnName("ContractReportTechnicalCorrectionIndicatorId");
            this.Property(t => t.ContractReportTechnicalCorrectionId).HasColumnName("ContractReportTechnicalCorrectionId");
            this.Property(t => t.ContractReportIndicatorId).HasColumnName("ContractReportIndicatorId");
            this.Property(t => t.ContractReportTechnicalId).HasColumnName("ContractReportTechnicalId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");

            this.Property(t => t.CorrectedApprovedPeriodAmountMen).HasColumnName("CorrectedApprovedPeriodAmountMen");
            this.Property(t => t.CorrectedApprovedPeriodAmountWomen).HasColumnName("CorrectedApprovedPeriodAmountWomen");
            this.Property(t => t.CorrectedApprovedPeriodAmountTotal).HasColumnName("CorrectedApprovedPeriodAmountTotal");

            this.Property(t => t.CorrectedApprovedCumulativeAmountMen).HasColumnName("CorrectedApprovedCumulativeAmountMen");
            this.Property(t => t.CorrectedApprovedCumulativeAmountWomen).HasColumnName("CorrectedApprovedCumulativeAmountWomen");
            this.Property(t => t.CorrectedApprovedCumulativeAmountTotal).HasColumnName("CorrectedApprovedCumulativeAmountTotal");

            this.Property(t => t.CorrectedApprovedResidueAmountMen).HasColumnName("CorrectedApprovedResidueAmountMen");
            this.Property(t => t.CorrectedApprovedResidueAmountWomen).HasColumnName("CorrectedApprovedResidueAmountWomen");
            this.Property(t => t.CorrectedApprovedResidueAmountTotal).HasColumnName("CorrectedApprovedResidueAmountTotal");

            this.Property(t => t.LastReportCorrectedCumulativeAmountMen).HasColumnName("LastReportCorrectedCumulativeAmountMen");
            this.Property(t => t.LastReportCorrectedCumulativeAmountWomen).HasColumnName("LastReportCorrectedCumulativeAmountWomen");
            this.Property(t => t.LastReportCorrectedCumulativeAmountTotal).HasColumnName("LastReportCorrectedCumulativeAmountTotal");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
