using Eumis.Public.Domain.Entities.Umis.Procedures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionEvaluation
    {
        public EvalSessionEvaluation()
        {
            this.EvalSessionEvaluationSheets = new List<EvalSessionEvaluationSheet>();
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionEvaluationId { get; set; }

        public int ProjectId { get; set; }

        public EvalSessionEvaluationCalculationType CalculationType { get; set; }

        public ProcedureEvalTableType EvalTableType { get; set; }

        public ProcedureEvalType EvalType { get; set; }

        public bool EvalIsPassed { get; set; }

        public decimal? EvalPoints { get; set; }

        public string EvalNote { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual EvalSession EvalSession { get; set; }

        public virtual ICollection<EvalSessionEvaluationSheet> EvalSessionEvaluationSheets { get; set; }
    }

    public class EvalSessionEvaluationMap : EntityTypeConfiguration<EvalSessionEvaluation>
    {
        public EvalSessionEvaluationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionEvaluationId});

            // Properties
            this.Property(t => t.EvalSessionEvaluationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProjectId)
                .IsRequired();

            this.Property(t => t.EvalTableType)
                .IsRequired();

            this.Property(t => t.CalculationType)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionEvaluations");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionEvaluationId).HasColumnName("EvalSessionEvaluationId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.EvalTableType).HasColumnName("EvalTableType");
            this.Property(t => t.CalculationType).HasColumnName("CalculationType");
            this.Property(t => t.EvalType).HasColumnName("EvalType");
            this.Property(t => t.EvalIsPassed).HasColumnName("EvalIsPassed");
            this.Property(t => t.EvalPoints).HasColumnName("EvalPoints");
            this.Property(t => t.EvalNote).HasColumnName("EvalNote");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsDeletedNote).HasColumnName("IsDeletedNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            //Relationships
            this.HasRequired(t => t.EvalSession)
                .WithMany(t => t.EvalSessionEvaluations)
                .HasForeignKey(t => t.EvalSessionId);
        }
    }
}
