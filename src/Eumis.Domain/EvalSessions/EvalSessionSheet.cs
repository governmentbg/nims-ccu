using Eumis.Common.Db;
using Eumis.Domain.Core;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EvalSessions
{
    public partial class EvalSessionSheet : INotificationEventEmitter
    {
        private static Sequence evalSessionSheetSequence = new Sequence("EvalSessionSheetSequence", "DbContext");

        public EvalSessionSheet()
        {
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
        }

        public EvalSessionSheet(
            int evalSessionId,
            int evalSessionUserId,
            int projectId,
            ProcedureEvalTableType evalTableType,
            DateTime currentDate,
            EvalSessionDistributionType distributionType,
            int? evalSessionDistributionId,
            string notes)
            : this()
        {
            this.EvalSessionSheetId = evalSessionSheetSequence.NextIntValue();

            this.EvalSessionId = evalSessionId;
            this.EvalSessionUserId = evalSessionUserId;
            this.ProjectId = projectId;
            this.EvalTableType = evalTableType;
            this.Status = EvalSessionSheetStatus.Draft;
            this.StatusDate = currentDate;
            this.CreateDate = currentDate;
            this.DistributionType = distributionType;
            this.EvalSessionDistributionId = evalSessionDistributionId;
            this.Notes = notes;
        }

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

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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

            // Relationships
            this.HasRequired(t => t.EvalSession)
                .WithMany(t => t.EvalSessionSheets)
                .HasForeignKey(t => t.EvalSessionId);
        }
    }
}
