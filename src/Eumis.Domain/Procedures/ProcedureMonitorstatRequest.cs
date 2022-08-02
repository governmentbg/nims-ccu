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
    public partial class ProcedureMonitorstatRequest : IAggregateRoot
    {
        public ProcedureMonitorstatRequest()
        {
        }

        public ProcedureMonitorstatRequest(int procedureId, DateTime? exectutionStartDate, DateTime? exectutionEndDate)
            : this()
        {
            this.ProcedureId = procedureId;
            this.ExecutionStartDate = exectutionStartDate;
            this.ExecutionEndDate = exectutionEndDate;

            var currentDate = DateTime.Now;
            this.Status = ProcedureMonitorstatRequestStatus.Draft;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ProcedureMonitorstatRequestId { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public ProcedureMonitorstatRequestStatus Status { get; set; }

        public DateTime? ExecutionStartDate { get; set; }

        public DateTime? ExecutionEndDate { get; set; }

        public Guid? MonitorstatInquiryGid { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureMonitorstatRequestMap : EntityTypeConfiguration<ProcedureMonitorstatRequest>
    {
        public ProcedureMonitorstatRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureMonitorstatRequestId);

            this.Property(t => t.ProcedureMonitorstatRequestId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureMonitorstatRequests");
            this.Property(t => t.ProcedureMonitorstatRequestId).HasColumnName("ProcedureMonitorstatRequestId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ExecutionStartDate).HasColumnName("ExecutionStartDate");
            this.Property(t => t.ExecutionEndDate).HasColumnName("ExecutionEndDate");
            this.Property(t => t.MonitorstatInquiryGid).HasColumnName("MonitorstatInquiryGid");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
