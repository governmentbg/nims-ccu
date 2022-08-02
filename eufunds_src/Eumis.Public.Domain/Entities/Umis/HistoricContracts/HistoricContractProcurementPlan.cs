using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.HistoricContracts
{
    public class HistoricContractProcurementPlan
    {
        public int HistoricContractProcurementPlanId { get; set; }

        public int HistoricContractId { get; set; }

        public string ProcurementPlanName { get; set; }

        public decimal Amount { get; set; }

        public virtual HistoricContract HistoricContract { get; set; }

        public virtual ICollection<HistoricContractProcurementPlanPosition> HistoricContractProcurementPlanPositions { get; set; }
    }

    public class HistoricContractProcurementPlanMap : EntityTypeConfiguration<HistoricContractProcurementPlan>
    {
        public HistoricContractProcurementPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoricContractProcurementPlanId);

            // Properties
            this.Property(t => t.HistoricContractProcurementPlanId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HistoricContractId)
                .IsRequired();

            this.Property(t => t.ProcurementPlanName)
                .IsRequired();

            this.Property(t => t.Amount)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("HistoricContractProcurementPlans");
            this.Property(t => t.HistoricContractProcurementPlanId).HasColumnName("HistoricContractProcurementPlanId");
            this.Property(t => t.HistoricContractId).HasColumnName("HistoricContractId");
            this.Property(t => t.ProcurementPlanName).HasColumnName("ProcurementPlanName");
            this.Property(t => t.Amount).HasColumnName("Amount");

            // Relationships
            this.HasRequired(t => t.HistoricContract)
                .WithMany(t => t.HistoricContractProcurementPlans)
                .HasForeignKey(d => d.HistoricContractId)
                .WillCascadeOnDelete();
        }
    }
}
