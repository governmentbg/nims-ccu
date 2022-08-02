using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureIndicator
    {
        public ProcedureIndicator()
        {
            this.IsActivated = false;
            this.IsActive = true;
        }

        public int ProcedureId { get; set; }
        public int IndicatorId { get; set; }
        public decimal? BaseTotalValue { get; set; }
        public decimal? BaseMenValue { get; set; }
        public decimal? BaseWomenValue { get; set; }
        public int? BaseYear { get; set; }
        public decimal? TargetTotalValue { get; set; }
        public decimal? TargetMenValue { get; set; }
        public decimal? TargetWomenValue { get; set; }
        public decimal? MilestoneTargetTotalValue { get; set; }
        public decimal? MilestoneTargetMenValue { get; set; }
        public decimal? MilestoneTargetWomenValue { get; set; }
        public string DataSource { get; set; }
        public string Description { get; set; }
        public bool IsActivated { get; set; }
        public bool IsActive { get; set; }
        public int SourceMapNodeId { get; set; }

        public virtual Procedure Procedure { get; set; }

        internal void SetAttributes(
            decimal? baseTotalValue,
            decimal? baseMenValue,
            decimal? baseWomenValue,
            int? baseYear,
            decimal? targetTotalValue,
            decimal? targetMenValue,
            decimal? targetWomenValue,
            decimal? milestoneTargetTotalValue,
            decimal? milestoneTargetMenValue,
            decimal? milestoneTargetWomenValue,
            string dataSource,
            string description)
        {
            this.BaseTotalValue = baseTotalValue;
            this.BaseMenValue = baseMenValue;
            this.BaseWomenValue = baseWomenValue;
            this.BaseYear = baseYear;
            this.TargetTotalValue = targetTotalValue;
            this.TargetMenValue = targetMenValue;
            this.TargetWomenValue = targetWomenValue;
            this.MilestoneTargetTotalValue = milestoneTargetTotalValue;
            this.MilestoneTargetMenValue = milestoneTargetMenValue;
            this.MilestoneTargetWomenValue = milestoneTargetWomenValue;
            this.DataSource = dataSource;
            this.Description = description;
        }
    }

    public class ProcedureIndicatorMap : EntityTypeConfiguration<ProcedureIndicator>
    {
        public ProcedureIndicatorMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProcedureId, t.IndicatorId });

            // Properties
            this.Property(t => t.ProcedureId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IndicatorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DataSource)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ProcedureIndicators");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.IndicatorId).HasColumnName("IndicatorId");
            this.Property(t => t.BaseTotalValue).HasColumnName("BaseTotalValue");
            this.Property(t => t.BaseMenValue).HasColumnName("BaseMenValue");
            this.Property(t => t.BaseWomenValue).HasColumnName("BaseWomenValue");
            this.Property(t => t.BaseYear).HasColumnName("BaseYear");
            this.Property(t => t.TargetTotalValue).HasColumnName("TargetTotalValue");
            this.Property(t => t.TargetMenValue).HasColumnName("TargetMenValue");
            this.Property(t => t.TargetWomenValue).HasColumnName("TargetWomenValue");
            this.Property(t => t.MilestoneTargetTotalValue).HasColumnName("MilestoneTargetTotalValue");
            this.Property(t => t.MilestoneTargetMenValue).HasColumnName("MilestoneTargetMenValue");
            this.Property(t => t.MilestoneTargetWomenValue).HasColumnName("MilestoneTargetWomenValue");
            this.Property(t => t.DataSource).HasColumnName("DataSource");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.SourceMapNodeId).HasColumnName("SourceMapNodeId");

            // Relationships
            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureIndicators)
                .HasForeignKey(d => d.ProcedureId);
        }
    }
}
