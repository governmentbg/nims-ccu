using Eumis.Public.Domain.Entities.Umis.Procedures;
using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionSheet
    {
        public int EvalSessionId { get; set; }

        public int EvalSessionSheetId { get; set; }

        public int EvalSessionUserId { get; set; }

        public int ProjectId { get; set; }

        public ProcedureEvalTableType EvalTableType { get; set; }

        public EvalSessionSheetStatus Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime StatusDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string Notes { get; set; }

        public int? EvalSessionDistributionId { get; set; }

        public EvalSessionDistributionType DistributionType { get; set; }

        public virtual EvalSession EvalSession { get; set; }

        public int? ContinuedEvalSessionSheetId { get; set; }
    }

    public class EvalSessionSheetMap : EntityTypeConfiguration<EvalSessionSheet>
    {
        public EvalSessionSheetMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionSheetId });

            // Properties
            this.Property(t => t.EvalSessionUserId)
                .IsRequired();

            this.Property(t => t.ProjectId)
                .IsRequired();

            this.Property(t => t.EvalTableType)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.DistributionType)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionSheets");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionSheetId).HasColumnName("EvalSessionSheetId");
            this.Property(t => t.EvalSessionUserId).HasColumnName("EvalSessionUserId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.EvalTableType).HasColumnName("EvalTableType");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.StatusDate).HasColumnName("StatusDate");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.EvalSessionDistributionId).HasColumnName("EvalSessionDistributionId");
            this.Property(t => t.DistributionType).HasColumnName("DistributionType");
            this.Property(t => t.ContinuedEvalSessionSheetId).HasColumnName("ContinuedEvalSessionSheetId");

            //Relationships
            this.HasRequired(t => t.EvalSession)
                .WithMany(t => t.EvalSessionSheets)
                .HasForeignKey(t => t.EvalSessionId);
        }
    }
}
