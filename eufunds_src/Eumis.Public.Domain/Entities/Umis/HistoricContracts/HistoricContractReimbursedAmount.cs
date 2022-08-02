using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.HistoricContracts
{
    public class HistoricContractReimbursedAmount
    {
        public int HistoricContractReimbursedAmountId { get; set; }

        public int HistoricContractId { get; set; }

        public DateTime ReimbursementDate { get; set; }

        public decimal? ReimbursedPrincipalEuAmount { get; set; }

        public decimal? ReimbursedPrincipalBgAmount { get; set; }

        public virtual HistoricContract HistoricContract { get; set; }
    }

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
