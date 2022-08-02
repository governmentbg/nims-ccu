using Eumis.Common.Db;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.HistoricContracts
{
    public class HistoricContractReimbursedAmount
    {
        private static Sequence historicContractReimbursedAmountSequence = new Sequence("HistoricContractReimbursedAmountSequence", "DbContext");

        private HistoricContractReimbursedAmount()
        {
        }

        public HistoricContractReimbursedAmount(
            int historicContractId,
            DateTime reimbursementDate,
            decimal? reimbursedPrincipalEuAmount,
            decimal? reimbursedPrincipalBgAmount)
        {
            this.HistoricContractReimbursedAmountId = historicContractReimbursedAmountSequence.NextIntValue();
            this.HistoricContractId = historicContractId;
            this.ReimbursementDate = reimbursementDate;
            this.ReimbursedPrincipalEuAmount = reimbursedPrincipalEuAmount;
            this.ReimbursedPrincipalBgAmount = reimbursedPrincipalBgAmount;
        }

        public int HistoricContractReimbursedAmountId { get; set; }

        public int HistoricContractId { get; set; }

        public DateTime ReimbursementDate { get; set; }

        public decimal? ReimbursedPrincipalEuAmount { get; set; }

        public decimal? ReimbursedPrincipalBgAmount { get; set; }

        public virtual HistoricContract HistoricContract { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class HistoricContractReimbursedAmountMap : EntityTypeConfiguration<HistoricContractReimbursedAmount>
    {
        public HistoricContractReimbursedAmountMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoricContractReimbursedAmountId);

            // Properties
            this.Property(t => t.HistoricContractReimbursedAmountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HistoricContractId)
                .IsRequired();

            this.Property(t => t.ReimbursementDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("HistoricContractReimbursedAmounts");
            this.Property(t => t.HistoricContractReimbursedAmountId).HasColumnName("HistoricContractReimbursedAmountId");
            this.Property(t => t.HistoricContractId).HasColumnName("HistoricContractId");
            this.Property(t => t.ReimbursementDate).HasColumnName("ReimbursementDate");
            this.Property(t => t.ReimbursedPrincipalEuAmount).HasColumnName("ReimbursedPrincipalEuAmount");
            this.Property(t => t.ReimbursedPrincipalBgAmount).HasColumnName("ReimbursedPrincipalBgAmount");

            // Relationships
            this.HasRequired(t => t.HistoricContract)
                .WithMany(t => t.HistoricContractReimbursedAmounts)
                .HasForeignKey(d => d.HistoricContractId)
                .WillCascadeOnDelete();
        }
    }
}
