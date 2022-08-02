using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Monitorstat
{
    public class MonitorstatReport
    {
        public MonitorstatReport()
        {
        }

        public int MonitorstatReportId { get; set; }

        public int MonitorstatSurveyId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public virtual MonitorstatSurvey Survey { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MonitorstaReportMap : EntityTypeConfiguration<MonitorstatReport>
    {
        public MonitorstaReportMap()
        {
            // Primary Key
            this.HasKey(t => t.MonitorstatReportId);

            // Properties
            this.Property(t => t.MonitorstatReportId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
               .IsRequired()
               .HasMaxLength(50);

            this.Property(t => t.Name)
               .IsRequired();

            this.Property(t => t.MonitorstatSurveyId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("MonitorstatReports");
            this.Property(t => t.MonitorstatReportId).HasColumnName("MonitorstatReportId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.MonitorstatSurveyId).HasColumnName("MonitorstatSurveyId");

            this.HasRequired(t => t.Survey)
                .WithMany(t => t.Reports)
                .HasForeignKey(t => t.MonitorstatSurveyId)
                .WillCascadeOnDelete();
        }
    }
}
