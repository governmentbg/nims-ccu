using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionStandingProject
    {
        public EvalSessionStandingProject()
        {
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionStandingId { get; set; }

        public int ProjectId { get; set; }

        public virtual EvalSessionStanding EvalSessionStanding { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EvalSessionStandingProjectMap : EntityTypeConfiguration<EvalSessionStandingProject>
    {
        public EvalSessionStandingProjectMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionStandingId, t.ProjectId });

            // Table & Column Mappings
            this.ToTable("EvalSessionStandingProjects");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionStandingId).HasColumnName("EvalSessionStandingId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");

            // Relationships
            this.HasRequired(t => t.EvalSessionStanding)
                .WithMany(t => t.Projects)
                .HasForeignKey(t => new { t.EvalSessionId, t.EvalSessionStandingId });
        }
    }
}
