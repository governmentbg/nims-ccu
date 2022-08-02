using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionProject
    {
        public EvalSessionProject()
        {
        }

        public int EvalSessionId { get; set; }

        public int ProjectId { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public virtual EvalSession EvalSession { get; set; }
    }

    public class EvalSessionProjectMap : EntityTypeConfiguration<EvalSessionProject>
    {
        public EvalSessionProjectMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.ProjectId });

            // Properties
            this.Property(t => t.IsDeleted)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionProjects");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsDeletedNote).HasColumnName("IsDeletedNote");

            //Relationships
            this.HasRequired(t => t.EvalSession)
                .WithMany(t => t.EvalSessionProjects)
                .HasForeignKey(t => t.EvalSessionId);
        }
    }
}
