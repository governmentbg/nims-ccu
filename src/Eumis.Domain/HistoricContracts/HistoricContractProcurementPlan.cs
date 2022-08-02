using Eumis.Common.Db;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.HistoricContracts
{
    public class HistoricContractProcurementPlan
    {
        private static Sequence historicContractProcurementPlanSequence = new Sequence("HistoricContractProcurementPlanSequence", "DbContext");

        private HistoricContractProcurementPlan()
        {
            this.HistoricContractProcurementPlanPositions = new List<HistoricContractProcurementPlanPosition>();
        }

        public HistoricContractProcurementPlan(
            int historicContractId,
            string procurementPlanName,
            decimal amount)
        {
            this.HistoricContractProcurementPlanId = historicContractProcurementPlanSequence.NextIntValue();
            this.HistoricContractId = historicContractId;
            this.ProcurementPlanName = procurementPlanName;
            this.Amount = amount;
        }

        public int HistoricContractProcurementPlanId { get; set; }

        public int HistoricContractId { get; set; }

        public string ProcurementPlanName { get; set; }

        public decimal Amount { get; set; }

        public virtual HistoricContract HistoricContract { get; set; }

        public virtual ICollection<HistoricContractProcurementPlanPosition> HistoricContractProcurementPlanPositions { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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
