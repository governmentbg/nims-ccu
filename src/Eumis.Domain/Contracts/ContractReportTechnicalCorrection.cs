using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportTechnicalCorrection : IAggregateRoot
    {
        private ContractReportTechnicalCorrection()
        {
        }

        public ContractReportTechnicalCorrection(
            int contractReportTechnicalId,
            int contractReportId,
            int contractId,
            int orderNum)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportTechnicalId = contractReportTechnicalId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;
            this.OrderNum = orderNum;
            this.Status = ContractReportTechnicalCorrectionStatus.Draft;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportTechnicalCorrectionId { get; set; }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public int OrderNum { get; set; }

        public ContractReportTechnicalCorrectionStatus Status { get; set; }

        public DateTime? CorrectionDate { get; set; }

        public Guid? BlobKey { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual Blob File { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportTechnicalCorrectionMap : EntityTypeConfiguration<ContractReportTechnicalCorrection>
    {
        public ContractReportTechnicalCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportTechnicalCorrectionId);

            // Properties
            this.Property(t => t.ContractReportTechnicalCorrectionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportTechnicalId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Status)
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
            this.ToTable("ContractReportTechnicalCorrections");
            this.Property(t => t.ContractReportTechnicalCorrectionId).HasColumnName("ContractReportTechnicalCorrectionId");
            this.Property(t => t.ContractReportTechnicalId).HasColumnName("ContractReportTechnicalId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CorrectionDate).HasColumnName("CorrectionDate");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(d => d.BlobKey);
        }
    }
}
