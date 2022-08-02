using Eumis.Domain.Monitorstat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Monitorstat
{
    public partial class MonitorstatSurvey : IAggregateRoot
    {
        public MonitorstatSurvey()
        {
            this.Reports = new List<MonitorstatReport>();
        }

        public MonitorstatSurvey(string code, string name, MonitorstatYear year)
            : base()
        {
            this.Code = code;
            this.Name = name;
            this.Year = year;

            var currentDateTime = DateTime.Now;
            this.CreateDate = currentDateTime;
            this.ModifyDate = currentDateTime;
        }

        public int MonitorstatSurveyId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public MonitorstatYear Year { get; set; }

        public ICollection<MonitorstatReport> Reports { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MonitorstatSurveyMap : EntityTypeConfiguration<MonitorstatSurvey>
    {
        public MonitorstatSurveyMap()
        {
            // Primary Key
            this.HasKey(t => t.MonitorstatSurveyId);

            // Properties
            this.Property(t => t.MonitorstatSurveyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
               .IsRequired()
               .HasMaxLength(50);

            this.Property(t => t.Name)
               .IsRequired();

            this.Property(t => t.Year)
               .IsRequired();

            this.Property(t => t.CreateDate)
               .IsRequired();

            // Table & Column Mappings
            this.ToTable("MonitorstatSurveys");
            this.Property(t => t.MonitorstatSurveyId).HasColumnName("MonitorstatSurveyId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
