using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EvalSessions
{
    public partial class EvalSession : IAggregateRoot, IEventEmitter
    {
        private EvalSession()
        {
            ((IEventEmitter)this).Events = new List<IDomainEvent>();

            this.EvalSessionUsers = new List<EvalSessionUser>();
            this.EvalSessionProjects = new List<EvalSessionProject>();
            this.EvalSessionSheets = new List<EvalSessionSheet>();
            this.EvalSessionDistributions = new List<EvalSessionDistribution>();
            this.EvalSessionEvaluations = new List<EvalSessionEvaluation>();
            this.EvalSessionDocuments = new List<EvalSessionDocument>();
            this.EvalSessionStandings = new List<EvalSessionStanding>();
            this.EvalSessionProjectStandings = new List<EvalSessionProjectStanding>();
            this.EvalSessionStandpoints = new List<EvalSessionStandpoint>();
            this.EvalSessionResults = new List<EvalSessionResult>();
        }

        public EvalSession(
            int procedureId,
            EvalSessionType evalSessionType,
            string sessionNum,
            string orderNum,
            DateTime? orderDate)
            : this()
        {
            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
            this.SessionDate = currentDate;

            this.ProcedureId = procedureId;
            this.EvalSessionStatus = EvalSessionStatus.Draft;
            this.EvalSessionType = evalSessionType;
            this.SessionNum = sessionNum;
            this.OrderNum = orderNum;
            this.OrderDate = orderDate;
        }

        public int EvalSessionId { get; set; }

        public int ProcedureId { get; set; }

        public EvalSessionStatus EvalSessionStatus { get; set; }

        public EvalSessionType EvalSessionType { get; set; }

        public string SessionNum { get; set; }

        public DateTime? SessionDate { get; set; }

        public string OrderNum { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<EvalSessionUser> EvalSessionUsers { get; set; }

        public virtual ICollection<EvalSessionProject> EvalSessionProjects { get; set; }

        public virtual ICollection<EvalSessionSheet> EvalSessionSheets { get; set; }

        public virtual ICollection<EvalSessionDistribution> EvalSessionDistributions { get; set; }

        public virtual ICollection<EvalSessionEvaluation> EvalSessionEvaluations { get; set; }

        public virtual ICollection<EvalSessionDocument> EvalSessionDocuments { get; set; }

        public virtual ICollection<EvalSessionStanding> EvalSessionStandings { get; set; }

        public virtual ICollection<EvalSessionProjectStanding> EvalSessionProjectStandings { get; set; }

        public virtual ICollection<EvalSessionStandpoint> EvalSessionStandpoints { get; set; }

        public virtual ICollection<EvalSessionResult> EvalSessionResults { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EvalSessionMap : EntityTypeConfiguration<EvalSession>
    {
        public EvalSessionMap()
        {
            // Primary Key
            this.HasKey(t => t.EvalSessionId);

            // Properties
            this.Property(t => t.EvalSessionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.EvalSessionStatus)
                .IsRequired();

            this.Property(t => t.EvalSessionType)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessions");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.EvalSessionStatus).HasColumnName("EvalSessionStatus");
            this.Property(t => t.EvalSessionType).HasColumnName("EvalSessionType");
            this.Property(t => t.SessionNum).HasColumnName("SessionNum");
            this.Property(t => t.SessionDate).HasColumnName("SessionDate");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum").HasMaxLength(50);
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
