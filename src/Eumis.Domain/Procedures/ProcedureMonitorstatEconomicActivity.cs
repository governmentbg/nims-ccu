using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Monitorstat;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureMonitorstatEconomicActivity : IAggregateRoot
    {
        public ProcedureMonitorstatEconomicActivity()
        {
        }

        public ProcedureMonitorstatEconomicActivity(int procedureId, MonitorstatYear year, ProcedureMonitorstatEconomicActivityType type)
        {
            this.ProcedureId = procedureId;
            this.Year = year;
            this.Type = type;
            this.Status = ProcedureMonitorstatEconomicActivityStatus.Draft;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ProcedureMonitorstatEconomicActivityId { get; set; }

        public int ProcedureId { get; set; }

        public MonitorstatYear Year { get; set; }

        public ProcedureMonitorstatEconomicActivityType Type { get; set; }

        public ProcedureMonitorstatEconomicActivityStatus Status { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureMonitorstatEconomicActivityMap : EntityTypeConfiguration<ProcedureMonitorstatEconomicActivity>
    {
        public ProcedureMonitorstatEconomicActivityMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureMonitorstatEconomicActivityId);

            this.Property(t => t.ProcedureMonitorstatEconomicActivityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.Year)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Status)
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
            this.ToTable("ProcedureMonitorstatEconomicActivities");
            this.Property(t => t.ProcedureMonitorstatEconomicActivityId).HasColumnName("ProcedureMonitorstatEconomicActivityId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
