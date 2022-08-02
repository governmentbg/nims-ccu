using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.HistoricContracts
{
    public class HistoricContractContractedAmount
    {
        public int HistoricContractContractedAmountId { get; set; }

        public int HistoricContractId { get; set; }

        public DateTime ContractedDate { get; set; }

        public decimal? ContractedEuAmount { get; set; }

        public decimal? ContractedBgAmount { get; set; }

        public decimal? ContractedSeftAmount { get; set; }

        public bool IsLast { get; set; }

        public virtual HistoricContract HistoricContract { get; set; }
    }

    public class HistoricContractContractedAmountMap : EntityTypeConfiguration<HistoricContractContractedAmount>
    {
        public HistoricContractContractedAmountMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoricContractContractedAmountId);

            // Properties
            this.Property(t => t.HistoricContractContractedAmountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HistoricContractId)
                .IsRequired();

            this.Property(t => t.ContractedDate)
                .IsRequired();

            this.Property(t => t.IsLast)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("HistoricContractContractedAmounts");
            this.Property(t => t.HistoricContractContractedAmountId).HasColumnName("HistoricContractContractedAmountId");
            this.Property(t => t.HistoricContractId).HasColumnName("HistoricContractId");
            this.Property(t => t.ContractedDate).HasColumnName("ContractedDate");
            this.Property(t => t.ContractedEuAmount).HasColumnName("ContractedEuAmount");
            this.Property(t => t.ContractedBgAmount).HasColumnName("ContractedBgAmount");
            this.Property(t => t.ContractedSeftAmount).HasColumnName("ContractedSeftAmount");
            this.Property(t => t.IsLast).HasColumnName("IsLast");

            // Relationships
            this.HasRequired(t => t.HistoricContract)
                .WithMany(t => t.HistoricContractContractedAmounts)
                .HasForeignKey(d => d.HistoricContractId)
                .WillCascadeOnDelete();
        }
    }
}
