using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Projects
{
    public partial class ProjectMonitorstatResponse
    {
        public ProjectMonitorstatResponse()
        {
        }

        public ProjectMonitorstatResponse(string fileName, Guid fileKey)
            : this()
        {
            this.FileName = fileName;
            this.FileKey = fileKey;

            this.ModifyDate = DateTime.Now;
        }

        public int ProjectMonitorstatResponseId { get; set; }

        public int ProjectMonitorstatRequestId { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual ProjectMonitorstatRequest MonitorstatRequest { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectMonitorstatResponseMap : EntityTypeConfiguration<ProjectMonitorstatResponse>
    {
        public ProjectMonitorstatResponseMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectMonitorstatResponseId);

            // Properties
            this.Property(t => t.ProjectMonitorstatResponseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProjectMonitorstatRequestId)
                .IsRequired();

            this.Property(t => t.FileName)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProjectMonitorstatResponses");
            this.Property(t => t.ProjectMonitorstatResponseId).HasColumnName("ProjectMonitorstatResponseId");
            this.Property(t => t.ProjectMonitorstatRequestId).HasColumnName("ProjectMonitorstatRequestId");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");

            this.HasRequired(t => t.MonitorstatRequest)
                .WithMany(t => t.MonitorstatResponses)
                .HasForeignKey(t => t.ProjectMonitorstatRequestId)
                .WillCascadeOnDelete();
        }
    }
}
