using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionDistributionUser
    {
        public EvalSessionDistributionUser()
        {
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionDistributionId { get; set; }

        public int EvalSessionUserId { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public virtual EvalSessionDistribution EvalSessionDistribution { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EvalSessionDistributionUserMap : EntityTypeConfiguration<EvalSessionDistributionUser>
    {
        public EvalSessionDistributionUserMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionDistributionId, t.EvalSessionUserId });

            // Properties
            this.Property(t => t.EvalSessionUserId)
                .IsRequired();

            this.Property(t => t.IsDeleted)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionDistributionUsers");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionDistributionId).HasColumnName("EvalSessionDistributionId");
            this.Property(t => t.EvalSessionUserId).HasColumnName("EvalSessionUserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsDeletedNote).HasColumnName("IsDeletedNote");

            // Relationships
            this.HasRequired(t => t.EvalSessionDistribution)
                .WithMany(t => t.EvalSessionDistributionUsers)
                .HasForeignKey(t => new { t.EvalSessionId, t.EvalSessionDistributionId });
        }
    }
}
