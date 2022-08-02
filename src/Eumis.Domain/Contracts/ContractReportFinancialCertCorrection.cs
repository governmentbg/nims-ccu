﻿using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancialCertCorrection : IAggregateRoot
    {
        private ContractReportFinancialCertCorrection()
        {
        }

        public ContractReportFinancialCertCorrection(
            int contractReportFinancialId,
            int contractReportId,
            int contractId,
            int orderNum)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportFinancialId = contractReportFinancialId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;
            this.OrderNum = orderNum;
            this.Status = ContractReportFinancialCertCorrectionStatus.Draft;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportFinancialCertCorrectionId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public int OrderNum { get; set; }

        public ContractReportFinancialCertCorrectionStatus Status { get; set; }

        public DateTime? CertCorrectionDate { get; set; }

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
    public class ContractReportFinancialCertCorrectionMap : EntityTypeConfiguration<ContractReportFinancialCertCorrection>
    {
        public ContractReportFinancialCertCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportFinancialCertCorrectionId);

            // Properties
            this.Property(t => t.ContractReportFinancialCertCorrectionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportFinancialId)
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
            this.ToTable("ContractReportFinancialCertCorrections");
            this.Property(t => t.ContractReportFinancialCertCorrectionId).HasColumnName("ContractReportFinancialCertCorrectionId");
            this.Property(t => t.ContractReportFinancialId).HasColumnName("ContractReportFinancialId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CertCorrectionDate).HasColumnName("CertCorrectionDate");
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