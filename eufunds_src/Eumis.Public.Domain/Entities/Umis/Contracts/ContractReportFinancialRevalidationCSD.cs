using Eumis.Public.Domain.Entities.Umis.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractReportFinancialRevalidationCSD : IAggregateRoot
    {

        private ContractReportFinancialRevalidationCSD()
        {
        }

        public ContractReportFinancialRevalidationCSD(
            int contractReportFinancialRevalidationId,
            int contractReportFinancialCSDBudgetItemId,
            int contractReportFinancialId,
            int contractReportId,
            int contractId)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportFinancialRevalidationId = contractReportFinancialRevalidationId;
            this.ContractReportFinancialCSDBudgetItemId = contractReportFinancialCSDBudgetItemId;
            this.ContractReportFinancialId = contractReportFinancialId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;

            this.Sign = Eumis.Public.Domain.Entities.Umis.Core.Sign.Positive;
            this.Status = ContractReportFinancialRevalidationCSDStatus.Draft;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportFinancialRevalidationCSDId { get; set; }
        public int ContractReportFinancialRevalidationId { get; set; }
        public int ContractReportFinancialCSDBudgetItemId { get; set; }
        public int ContractReportFinancialId { get; set; }
        public int ContractReportId { get; set; }
        public int ContractId { get; set; }
        public Guid Gid { get; set; }

        public Sign? Sign { get; set; }
        public ContractReportFinancialRevalidationCSDStatus Status { get; set; }
        public string Notes { get; set; }
        public int? CheckedByUserId { get; set; }
        public DateTime? CheckedDate { get; set; }

        public decimal? RevalidatedEuAmount { get; set; }
        public decimal? RevalidatedBgAmount { get; set; }
        public decimal? RevalidatedBfpTotalAmount { get; set; }
        public decimal? RevalidatedSelfAmount { get; set; }
        public decimal? RevalidatedTotalAmount { get; set; }

        public int? CertReportId { get; set; }

        public ContractReportFinancialRevalidationCSDCertStatus? CertStatus { get; set; }
        public int? CertCheckedByUserId { get; set; }
        public DateTime? CertCheckedDate { get; set; }
        public decimal? UncertifiedRevalidatedEuAmount { get; set; }
        public decimal? UncertifiedRevalidatedBgAmount { get; set; }
        public decimal? UncertifiedRevalidatedBfpTotalAmount { get; set; }
        public decimal? UncertifiedRevalidatedSelfAmount { get; set; }
        public decimal? UncertifiedRevalidatedTotalAmount { get; set; }

        public decimal? CertifiedRevalidatedEuAmount { get; set; }
        public decimal? CertifiedRevalidatedBgAmount { get; set; }
        public decimal? CertifiedRevalidatedBfpTotalAmount { get; set; }
        public decimal? CertifiedRevalidatedSelfAmount { get; set; }
        public decimal? CertifiedRevalidatedTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }
    }

    public class ContractReportFinancialRevalidationCSDMap : EntityTypeConfiguration<ContractReportFinancialRevalidationCSD>
    {
        public ContractReportFinancialRevalidationCSDMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportFinancialRevalidationCSDId);

            // Properties
            this.Property(t => t.ContractReportFinancialRevalidationCSDId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportFinancialRevalidationId)
                .IsRequired();

            this.Property(t => t.ContractReportFinancialCSDBudgetItemId)
                .IsRequired();

            this.Property(t => t.ContractReportFinancialId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
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
            this.ToTable("ContractReportFinancialRevalidationCSDs");
            this.Property(t => t.ContractReportFinancialRevalidationCSDId).HasColumnName("ContractReportFinancialRevalidationCSDId");
            this.Property(t => t.ContractReportFinancialRevalidationId).HasColumnName("ContractReportFinancialRevalidationId");
            this.Property(t => t.ContractReportFinancialCSDBudgetItemId).HasColumnName("ContractReportFinancialCSDBudgetItemId");
            this.Property(t => t.ContractReportFinancialId).HasColumnName("ContractReportFinancialId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.Sign).HasColumnName("Sign");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");

            this.Property(t => t.RevalidatedEuAmount).HasColumnName("RevalidatedEuAmount");
            this.Property(t => t.RevalidatedBgAmount).HasColumnName("RevalidatedBgAmount");
            this.Property(t => t.RevalidatedBfpTotalAmount).HasColumnName("RevalidatedBfpTotalAmount");
            this.Property(t => t.RevalidatedSelfAmount).HasColumnName("RevalidatedSelfAmount");
            this.Property(t => t.RevalidatedTotalAmount).HasColumnName("RevalidatedTotalAmount");

            this.Property(t => t.CertReportId).HasColumnName("CertReportId");

            this.Property(t => t.CertStatus).HasColumnName("CertStatus");
            this.Property(t => t.CertCheckedByUserId).HasColumnName("CertCheckedByUserId");
            this.Property(t => t.CertCheckedDate).HasColumnName("CertCheckedDate");
            this.Property(t => t.UncertifiedRevalidatedEuAmount).HasColumnName("UncertifiedRevalidatedEuAmount");
            this.Property(t => t.UncertifiedRevalidatedBgAmount).HasColumnName("UncertifiedRevalidatedBgAmount");
            this.Property(t => t.UncertifiedRevalidatedBfpTotalAmount).HasColumnName("UncertifiedRevalidatedBfpTotalAmount");
            this.Property(t => t.UncertifiedRevalidatedSelfAmount).HasColumnName("UncertifiedRevalidatedSelfAmount");
            this.Property(t => t.UncertifiedRevalidatedTotalAmount).HasColumnName("UncertifiedRevalidatedTotalAmount");

            this.Property(t => t.CertifiedRevalidatedEuAmount).HasColumnName("CertifiedRevalidatedEuAmount");
            this.Property(t => t.CertifiedRevalidatedBgAmount).HasColumnName("CertifiedRevalidatedBgAmount");
            this.Property(t => t.CertifiedRevalidatedBfpTotalAmount).HasColumnName("CertifiedRevalidatedBfpTotalAmount");
            this.Property(t => t.CertifiedRevalidatedSelfAmount).HasColumnName("CertifiedRevalidatedSelfAmount");
            this.Property(t => t.CertifiedRevalidatedTotalAmount).HasColumnName("CertifiedRevalidatedTotalAmount");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
