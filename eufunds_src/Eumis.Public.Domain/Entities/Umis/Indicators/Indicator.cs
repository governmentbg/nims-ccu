using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;

namespace Eumis.Public.Domain.Entities.Umis.Indicators
{
    public partial class Indicator : IAggregateRoot
    {
        protected Indicator()
        {
        }

        public Indicator(
            int programmeId,
            int measureId,
            string code,
            string name,
            IndicatorType type,
            IndicatorKind kind,
            IndicatorTrend trend,
            IndicatorAggregatedReport aggregatedReport,
            IndicatorAggregatedTarget aggregatedTarget,
            bool hasGenderDivision,
            bool hasQualitativeTarget,
            IndicatorReportingType reportingType)
        {
            var currentDate = DateTime.Now;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
            this.Gid = Guid.NewGuid();

            this.ProgrammeId = programmeId;
            this.MeasureId = measureId;
            this.Code = code;
            this.Name = name;
            this.Type = type;
            this.Kind = kind;
            this.Trend = trend;
            this.AggregatedReport = aggregatedReport;
            this.AggregatedTarget = aggregatedTarget;
            this.HasGenderDivision = hasGenderDivision;
            this.HasQualitativeTarget = hasQualitativeTarget;
            this.ReportingType = reportingType;
        }

        public int IndicatorId { get; set; }
        public int ProgrammeId { get; set; }
        public int MeasureId { get; set; }
        public Guid Gid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public IndicatorType Type { get; set; }
        public IndicatorKind Kind { get; set; }
        public IndicatorTrend Trend { get; set; }
        public IndicatorAggregatedReport AggregatedReport { get; set; }
        public IndicatorAggregatedTarget AggregatedTarget { get; set; }
        public bool HasGenderDivision { get; set; }
        public bool HasQualitativeTarget { get; set; }
        public IndicatorReportingType ReportingType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }
    }

    public class IndicatorMap : EntityTypeConfiguration<Indicator>
    {
        public IndicatorMap()
        {
            // Primary Key
            this.HasKey(t => t.IndicatorId);

            this.Property(t => t.IndicatorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Indicators");
            this.Property(t => t.IndicatorId).HasColumnName("IndicatorId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.MeasureId).HasColumnName("MeasureId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Kind).HasColumnName("Kind");
            this.Property(t => t.Trend).HasColumnName("Trend");
            this.Property(t => t.AggregatedReport).HasColumnName("AggregatedReport");
            this.Property(t => t.AggregatedTarget).HasColumnName("AggregatedTarget");
            this.Property(t => t.HasGenderDivision).HasColumnName("HasGenderDivision");
            this.Property(t => t.HasQualitativeTarget).HasColumnName("HasQualitativeTarget");
            this.Property(t => t.ReportingType).HasColumnName("ReportingType");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
