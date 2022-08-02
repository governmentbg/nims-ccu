using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public partial class EvalSessionReport : IAggregateRoot
    {
        public EvalSessionReport()
        {
            this.Projects = new List<EvalSessionReportProject>();
        }

        public EvalSessionReport(
            int evalSessionId,
            string regNumber,
            EvalSessionReportType type,
            string description)
        {
            this.EvalSessionId = evalSessionId;
            this.RegNumber = regNumber;
            this.Type = type;
            this.Description = description;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;

            this.Projects = new List<EvalSessionReportProject>();
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionReportId { get; set; }

        public string RegNumber { get; set; }

        public EvalSessionReportType Type { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<EvalSessionReportProject> Projects { get; set; }
    }

    public class EvalSessionReportMap : EntityTypeConfiguration<EvalSessionReport>
    {
        public EvalSessionReportMap()
        {
            // Primary Key
            this.HasKey(t => t.EvalSessionReportId);

            // Properties
            this.Property(t => t.EvalSessionReportId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.EvalSessionId)
                .IsRequired();
            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.Type)
                .IsRequired();
            this.Property(t => t.Description)
                .IsRequired();
            this.Property(t => t.IsDeleted)
                .IsRequired();
            this.Property(t => t.CreateDate)
                .IsRequired();
            this.Property(t => t.ModifyDate)
                .IsRequired();
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EvalSessionReports");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionReportId).HasColumnName("EvalSessionReportId");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsDeletedNote).HasColumnName("IsDeletedNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
