using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionProjectStandingEvaluation
    {
        public EvalSessionProjectStandingEvaluation()
        {
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionProjectStandingId { get; set; }

        public int EvalSessionEvaluationId { get; set; }

        public virtual EvalSessionProjectStanding EvalSessionProjectStanding { get; set; }
    }

    public class EvalSessionProjectStandingEvaluationMap : EntityTypeConfiguration<EvalSessionProjectStandingEvaluation>
    {
        public EvalSessionProjectStandingEvaluationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionProjectStandingId, t.EvalSessionEvaluationId });

            // Table & Column Mappings
            this.ToTable("EvalSessionProjectStandingEvaluations");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionProjectStandingId).HasColumnName("EvalSessionProjectStandingId");
            this.Property(t => t.EvalSessionEvaluationId).HasColumnName("EvalSessionEvaluationId");

            //Relationships
            this.HasRequired(t => t.EvalSessionProjectStanding)
                .WithMany(t => t.EvalSessionProjectStandingEvaluations)
                .HasForeignKey(t => new { t.EvalSessionId, t.EvalSessionProjectStandingId });
        }
    }
}
