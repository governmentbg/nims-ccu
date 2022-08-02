using Eumis.Common.Db;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.HistoricContracts
{
    public class HistoricContractProcurementPlanPosition
    {
        private static Sequence historicContractProcurementPlanPositionSequence = new Sequence("HistoricContractProcurementPlanPositionSequence", "DbContext");

        private HistoricContractProcurementPlanPosition()
        {
        }

        public HistoricContractProcurementPlanPosition(
            int historicContractProcurementPlanId,
            string positionName)
        {
            this.HistoricContractProcurementPlanPositionId = historicContractProcurementPlanPositionSequence.NextIntValue();
            this.HistoricContractProcurementPlanId = historicContractProcurementPlanId;
            this.PositionName = positionName;
        }

        public int HistoricContractProcurementPlanPositionId { get; set; }

        public int HistoricContractProcurementPlanId { get; set; }

        public string PositionName { get; set; }

        public virtual HistoricContractProcurementPlan HistoricContractProcurementPlan { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class HistoricContractProcurementPlanPositionMap : EntityTypeConfiguration<HistoricContractProcurementPlanPosition>
    {
        public HistoricContractProcurementPlanPositionMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoricContractProcurementPlanPositionId);

            // Properties
            this.Property(t => t.HistoricContractProcurementPlanPositionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HistoricContractProcurementPlanId)
                .IsRequired();

            this.Property(t => t.PositionName)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("HistoricContractProcurementPlanPositions");
            this.Property(t => t.HistoricContractProcurementPlanPositionId).HasColumnName("HistoricContractProcurementPlanPositionId");
            this.Property(t => t.HistoricContractProcurementPlanId).HasColumnName("HistoricContractProcurementPlanId");
            this.Property(t => t.PositionName).HasColumnName("PositionName");

            // Relationships
            this.HasRequired(t => t.HistoricContractProcurementPlan)
                .WithMany(t => t.HistoricContractProcurementPlanPositions)
                .HasForeignKey(d => d.HistoricContractProcurementPlanId)
                .WillCascadeOnDelete();
        }
    }
}
