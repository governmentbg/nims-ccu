using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.ProgrammePriorities;

namespace Eumis.Domain.OperationalMap.MapNodes
{
    public class MapNodeBudget
    {
        public MapNodeBudget()
        {
        }

        public int MapNodeId { get; set; }

        public int BudgetPeriodId { get; set; }

        public int ProgrammeId { get; set; }

        public decimal EuAmount { get; set; }

        public decimal BgAmount { get; set; }

        public decimal EuReservedAmount { get; set; }

        public decimal BgReservedAmount { get; set; }

        public decimal NextThreeWithAdvances { get; set; }

        public decimal NextThreeWithoutAdvances { get; set; }

        public virtual ProgrammePriority ProgrammePriority { get; set; }

        internal void SetAttributes(
            decimal euAmount,
            decimal bgAmount,
            decimal euReservedAmount,
            decimal bgReservedAmount,
            decimal nextThreeWithAdvances,
            decimal nextThreeWithoutAdvances)
        {
            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.EuReservedAmount = euReservedAmount;
            this.BgReservedAmount = bgReservedAmount;
            this.NextThreeWithAdvances = nextThreeWithAdvances;
            this.NextThreeWithoutAdvances = nextThreeWithoutAdvances;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MapNodeBudgetMap : EntityTypeConfiguration<MapNodeBudget>
    {
        public MapNodeBudgetMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MapNodeId, t.BudgetPeriodId });

            // Properties
            this.Property(t => t.MapNodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BudgetPeriodId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MapNodeBudgets");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.BudgetPeriodId).HasColumnName("BudgetPeriodId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.EuAmount).HasColumnName("EuAmount");
            this.Property(t => t.BgAmount).HasColumnName("BgAmount");
            this.Property(t => t.EuReservedAmount).HasColumnName("EuReservedAmount");
            this.Property(t => t.BgReservedAmount).HasColumnName("BgReservedAmount");
            this.Property(t => t.NextThreeWithAdvances).HasColumnName("NextThreeWithAdvances");
            this.Property(t => t.NextThreeWithoutAdvances).HasColumnName("NextThreeWithoutAdvances");

            // Relationships
            this.HasRequired(t => t.ProgrammePriority)
                .WithMany(t => t.ProgrammePriorityBudgets)
                .HasForeignKey(d => d.MapNodeId)
                .WillCascadeOnDelete();
        }
    }
}
