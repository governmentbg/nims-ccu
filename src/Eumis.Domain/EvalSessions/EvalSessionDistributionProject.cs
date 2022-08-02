using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionDistributionProject
    {
        public EvalSessionDistributionProject()
        {
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionDistributionId { get; set; }

        public int ProjectId { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public virtual EvalSessionDistribution EvalSessionDistribution { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EvalSessionDistributionProjectMap : EntityTypeConfiguration<EvalSessionDistributionProject>
    {
        public EvalSessionDistributionProjectMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionDistributionId, t.ProjectId });

            // Properties
            this.Property(t => t.ProjectId)
                .IsRequired();

            this.Property(t => t.IsDeleted)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionDistributionProjects");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionDistributionId).HasColumnName("EvalSessionDistributionId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsDeletedNote).HasColumnName("IsDeletedNote");

            // Relationships
            this.HasRequired(t => t.EvalSessionDistribution)
                .WithMany(t => t.EvalSessionDistributionProjects)
                .HasForeignKey(t => new { t.EvalSessionId, t.EvalSessionDistributionId });
        }
    }
}
