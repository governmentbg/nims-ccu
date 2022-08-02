using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionStandpoint
    {
        public EvalSessionStandpoint()
        {
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionStandpointId { get; set; }

        public int EvalSessionUserId { get; set; }

        public int ProjectId { get; set; }

        public string Note { get; set; }

        public DateTime CreateDate { get; set; }

        public EvalSessionStandpointStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public string DeleteNote { get; set; }

        public virtual EvalSession EvalSession { get; set; }
    }

    public class EvalSessionStandpointMap : EntityTypeConfiguration<EvalSessionStandpoint>
    {
        public EvalSessionStandpointMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionStandpointId });

            // Properties
            this.Property(t => t.EvalSessionStandpointId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.EvalSessionUserId)
                .IsRequired();

            this.Property(t => t.ProjectId)
                .IsRequired();

            this.Property(t => t.Note)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.StatusDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionStandpoints");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionStandpointId).HasColumnName("EvalSessionStandpointId");
            this.Property(t => t.EvalSessionUserId).HasColumnName("EvalSessionUserId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusDate).HasColumnName("StatusDate");
            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");

            //Relationships
            this.HasRequired(t => t.EvalSession)
                .WithMany(t => t.EvalSessionStandpoints)
                .HasForeignKey(t => t.EvalSessionId);
        }
    }
}
