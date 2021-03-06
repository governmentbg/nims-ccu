using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionEvaluationSheet
    {
        public EvalSessionEvaluationSheet()
        {
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionEvaluationId { get; set; }

        public int EvalSessionSheetId { get; set; }

        public virtual EvalSessionEvaluation EvalSessionEvaluation { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EvalSessionEvaluationSheetMap : EntityTypeConfiguration<EvalSessionEvaluationSheet>
    {
        public EvalSessionEvaluationSheetMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionEvaluationId, t.EvalSessionSheetId });

            // Table & Column Mappings
            this.ToTable("EvalSessionEvaluationSheets");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionEvaluationId).HasColumnName("EvalSessionEvaluationId");
            this.Property(t => t.EvalSessionSheetId).HasColumnName("EvalSessionSheetId");

            // Relationships
            this.HasRequired(t => t.EvalSessionEvaluation)
                .WithMany(t => t.EvalSessionEvaluationSheets)
                .HasForeignKey(t => new { t.EvalSessionId, t.EvalSessionEvaluationId });
        }
    }
}
