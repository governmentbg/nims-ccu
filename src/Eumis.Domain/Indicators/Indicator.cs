using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Indicators
{
    public partial class Indicator : IAggregateRoot, INotificationEventEmitter
    {
        protected Indicator()
        {
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
        }

        public Indicator(
            int programmeId,
            int measureId,
            int indicatorItemTypeId,
            string name,
            bool hasGenderDivision)
            : this()
        {
            var currentDate = DateTime.Now;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
            this.Gid = Guid.NewGuid();

            this.ProgrammeId = programmeId;
            this.MeasureId = measureId;
            this.IndicatorItemTypeId = indicatorItemTypeId;
            this.Name = name;
            this.HasGenderDivision = hasGenderDivision;
        }

        public int IndicatorId { get; set; }

        public int IndicatorItemTypeId { get; set; }

        public int ProgrammeId { get; set; }

        public int MeasureId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public bool HasGenderDivision { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IndicatorMap : EntityTypeConfiguration<Indicator>
    {
        public IndicatorMap()
        {
            // Primary Key
            this.HasKey(t => t.IndicatorId);

            this.Property(t => t.IndicatorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.IndicatorItemTypeId)
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
            this.Property(t => t.IndicatorItemTypeId).HasColumnName("IndicatorItemTypeId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.HasGenderDivision).HasColumnName("HasGenderDivision");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
