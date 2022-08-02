using Eumis.Public.Domain.Entities.Umis.Procedures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionDistribution
    {
        public EvalSessionDistribution()
        {
            this.EvalSessionDistributionUsers = new List<EvalSessionDistributionUser>();
            this.EvalSessionDistributionProjects = new List<EvalSessionDistributionProject>();
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionDistributionId { get; set; }

        public ProcedureEvalTableType EvalTableType { get; set; }

        public string Code { get; set; }

        public DateTime CreateDate { get; set; }

        public EvalSessionDistributionStatus Status { get; set; }

        public string StatusNote { get; set; }

        public int AssessorsPerProject { get; set; }

        public virtual EvalSession EvalSession { get; set; }

        public virtual ICollection<EvalSessionDistributionUser> EvalSessionDistributionUsers { get; set; }

        public virtual ICollection<EvalSessionDistributionProject> EvalSessionDistributionProjects { get; set; }
    }

    public class EvalSessionDistributionMap : EntityTypeConfiguration<EvalSessionDistribution>
    {
        public EvalSessionDistributionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionDistributionId });

            // Properties
            this.Property(t => t.EvalSessionDistributionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.EvalTableType)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.AssessorsPerProject)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionDistributions");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionDistributionId).HasColumnName("EvalSessionDistributionId");
            this.Property(t => t.EvalTableType).HasColumnName("EvalTableType");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.AssessorsPerProject).HasColumnName("AssessorsPerProject");

            //Relationships
            this.HasRequired(t => t.EvalSession)
                .WithMany(t => t.EvalSessionDistributions)
                .HasForeignKey(t => t.EvalSessionId);
        }
    }
}
