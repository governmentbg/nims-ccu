using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionUser
    {
        public EvalSessionUser()
        {
        }

        public int EvalSessionUserId { get; set; }

        public int EvalSessionId { get; set; }

        public int UserId { get; set; }

        public EvalSessionUserType Type { get; set; }

        public string Position { get; set; }

        public virtual EvalSession EvalSession { get; set; }

        internal void SetAttributes(string position)
        {
            this.Position = position;
        }

    }

    public class EvalSessionUserMap : EntityTypeConfiguration<EvalSessionUser>
    {
        public EvalSessionUserMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionUserId });

            // Properties
            this.Property(t => t.EvalSessionUserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Type)
                .IsRequired();
            this.Property(t => t.Position)
                .HasMaxLength(300)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("EvalSessionUsers");
            this.Property(t => t.EvalSessionUserId).HasColumnName("EvalSessionUserId");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Position).HasColumnName("Position");

            //Relationships
            this.HasRequired(t => t.EvalSession)
                .WithMany(t => t.EvalSessionUsers)
                .HasForeignKey(t => t.EvalSessionId)
                .WillCascadeOnDelete();
        }
    }
}
