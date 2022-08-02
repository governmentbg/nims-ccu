using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionStanding
    {
        public EvalSessionStanding()
        {
            this.Projects = new List<EvalSessionStandingProject>();
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionStandingId { get; set; }

        public string Code { get; set; }

        public bool IsPreliminary { get; set; }

        public int? PreliminaryBudgetPercentage { get; set; }

        public EvalSessionStandingStatus Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime StatusDate { get; set; }

        public virtual EvalSession EvalSession { get; set; }

        public virtual ICollection<EvalSessionStandingProject> Projects { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EvalSessionStandingMap : EntityTypeConfiguration<EvalSessionStanding>
    {
        public EvalSessionStandingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionStandingId });

            // Properties
            this.Property(t => t.EvalSessionStandingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("EvalSessionStandings");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionStandingId).HasColumnName("EvalSessionStandingId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.IsPreliminary).HasColumnName("IsPreliminary");
            this.Property(t => t.PreliminaryBudgetPercentage).HasColumnName("PreliminaryBudgetPercentage");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.StatusDate).HasColumnName("StatusDate");

            // Relationships
            this.HasRequired(t => t.EvalSession)
                .WithMany(t => t.EvalSessionStandings)
                .HasForeignKey(t => t.EvalSessionId);
        }
    }
}
