using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts
{
    public abstract partial class ReimbursedAmount : IAggregateRoot
    {
        public int ReimbursedAmountId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public FinanceSource? FinanceSource { get; set; }

        public int ContractId { get; set; }

        public abstract ReimbursedAmountDiscriminator Discriminator { get; }

        public ReimbursedAmountStatus Status { get; set; }

        public DateTime ReimbursementDate { get; set; }

        public ReimbursementType Type { get; set; }

        public Reimbursement Reimbursement { get; set; }

        public string RegNumber { get; set; }

        public BfpAmount PrincipalBfp { get; set; }

        public BfpAmount InterestBfp { get; set; }

        public string Comment { get; set; }

        public bool ShouldInfluencePaidAmounts { get; set; }

        public int? CertReportId { get; set; }

        public bool IsActivated { get; set; }

        public string IsDeletedNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class ReimbursedAmountMap : EntityTypeConfiguration<ReimbursedAmount>
    {
        public ReimbursedAmountMap()
        {
            // Primary Key
            this.HasKey(t => t.ReimbursedAmountId);

            // Properties
            this.Property(t => t.ReimbursedAmountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.ContractId)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.ReimbursementDate)
                .IsRequired();
            this.Property(t => t.Type)
                .IsRequired();
            this.Property(t => t.Reimbursement)
                .IsRequired();
            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.IsActivated)
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
            this.ToTable("ReimbursedAmounts");
            this.Property(t => t.ReimbursedAmountId).HasColumnName("ReimbursedAmountId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.FinanceSource).HasColumnName("FinanceSource");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ReimbursementDate).HasColumnName("ReimbursementDate");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Reimbursement).HasColumnName("Reimbursement");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");

            this.Property(t => t.PrincipalBfp.EuAmount).HasColumnName("PrincipalBfpEuAmount");
            this.Property(t => t.PrincipalBfp.BgAmount).HasColumnName("PrincipalBfpBgAmount");
            this.Property(t => t.PrincipalBfp.TotalAmount).HasColumnName("PrincipalBfpTotalAmount");

            this.Property(t => t.InterestBfp.EuAmount).HasColumnName("InterestBfpEuAmount");
            this.Property(t => t.InterestBfp.BgAmount).HasColumnName("InterestBfpBgAmount");
            this.Property(t => t.InterestBfp.TotalAmount).HasColumnName("InterestBfpTotalAmount");

            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.ShouldInfluencePaidAmounts).HasColumnName("ShouldInfluencePaidAmounts");

            this.Property(t => t.CertReportId).HasColumnName("CertReportId");

            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsDeletedNote).HasColumnName("IsDeletedNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            this.Map<DebtReimbursedAmount>    (t => t.Requires("Discriminator").HasValue((int)ReimbursedAmountDiscriminator.Debt    ));
            this.Map<ContractReimbursedAmount>(t => t.Requires("Discriminator").HasValue((int)ReimbursedAmountDiscriminator.Contract));
        }
    }
}
