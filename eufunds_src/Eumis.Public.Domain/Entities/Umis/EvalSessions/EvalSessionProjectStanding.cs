using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionProjectStanding
    {
        public EvalSessionProjectStanding()
        {
            this.EvalSessionProjectStandingEvaluations = new List<EvalSessionProjectStandingEvaluation>();
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionProjectStandingId { get; set; }

        public int ProjectId { get; set; }

        public bool IsPreliminary { get; set; }

        public int? OrderNum { get; set; }

        public EvalSessionProjectStandingStatus Status { get; set; }

        public decimal? GrandAmount { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public string Notes { get; set; }

        public int? EvalSessionStandingId { get; set; }

        public DateTime CreateDate { get; set; }

        public int ProjectVersionXmlId { get; set; }

        public int? RejectionReasonId { get; set; }

        public virtual EvalSession EvalSession { get; set; }

        public virtual ICollection<EvalSessionProjectStandingEvaluation> EvalSessionProjectStandingEvaluations { get; set; }
    }

    public class EvalSessionProjectStandingMap : EntityTypeConfiguration<EvalSessionProjectStanding>
    {
        public EvalSessionProjectStandingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionProjectStandingId });

            // Properties
            this.Property(t => t.EvalSessionProjectStandingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("EvalSessionProjectStandings");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionProjectStandingId).HasColumnName("EvalSessionProjectStandingId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.IsPreliminary).HasColumnName("IsPreliminary");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.GrandAmount).HasColumnName("GrandAmount");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsDeletedNote).HasColumnName("IsDeletedNote");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.EvalSessionStandingId).HasColumnName("EvalSessionStandingId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ProjectVersionXmlId).HasColumnName("ProjectVersionXmlId");
            this.Property(t => t.RejectionReasonId).HasColumnName("RejectionReasonId");

            //Relationships
            this.HasRequired(t => t.EvalSession)
                .WithMany(t => t.EvalSessionProjectStandings)
                .HasForeignKey(t => t.EvalSessionId);
        }
    }
}
