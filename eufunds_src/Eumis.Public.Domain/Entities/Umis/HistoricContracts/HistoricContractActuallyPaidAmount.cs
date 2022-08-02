using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.HistoricContracts
{
    public class HistoricContractActuallyPaidAmount
    {
        public int HistoricContractActuallyPaidAmountId { get; set; }

        public int HistoricContractId { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal? PaidEuAmount { get; set; }

        public decimal? PaidBgAmount { get; set; }

        public virtual HistoricContract HistoricContract { get; set; }
    }

    public class HistoricContractActuallyPaidAmountMap : EntityTypeConfiguration<HistoricContractActuallyPaidAmount>
    {
        public HistoricContractActuallyPaidAmountMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoricContractActuallyPaidAmountId);

            // Properties
            this.Property(t => t.HistoricContractActuallyPaidAmountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HistoricContractId)
                .IsRequired();

            this.Property(t => t.PaymentDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("HistoricContractActuallyPaidAmounts");
            this.Property(t => t.HistoricContractActuallyPaidAmountId).HasColumnName("HistoricContractActuallyPaidAmountId");
            this.Property(t => t.HistoricContractId).HasColumnName("HistoricContractId");
            this.Property(t => t.PaymentDate).HasColumnName("PaymentDate");
            this.Property(t => t.PaidEuAmount).HasColumnName("PaidEuAmount");
            this.Property(t => t.PaidBgAmount).HasColumnName("PaidBgAmount");

            // Relationships
            this.HasRequired(t => t.HistoricContract)
                .WithMany(t => t.HistoricContractActuallyPaidAmounts)
                .HasForeignKey(d => d.HistoricContractId)
                .WillCascadeOnDelete();
        }
    }
}
