using Eumis.Domain.Monitorstat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures
{
    public class ProcedureMonitorstatDocument : IAggregateRoot
    {
        public int ProcedureMonitorstatDocumentId { get; set; }

        public int ProcedureId { get; set; }

        public ProcedureMonitorstatDocumentStatus Status { get; set; }

        public MonitorstatYear Year { get; set; }

        public int MonitorstatSurveyId { get; set; }

        public int MonitorstatReportId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureMonitorstatDocumentMap : EntityTypeConfiguration<ProcedureMonitorstatDocument>
    {
        public ProcedureMonitorstatDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureMonitorstatDocumentId);

            this.Property(t => t.ProcedureMonitorstatDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.MonitorstatSurveyId)
                .IsRequired();

            this.Property(t => t.MonitorstatReportId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureMonitorstatDocuments");
            this.Property(t => t.ProcedureMonitorstatDocumentId).HasColumnName("ProcedureMonitorstatDocumentId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");

            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.MonitorstatSurveyId).HasColumnName("MonitorstatSurveyId");
            this.Property(t => t.MonitorstatReportId).HasColumnName("MonitorstatReportId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
