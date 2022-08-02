using Eumis.Domain.NonAggregates;
using Eumis.Domain.Notifications.NotificationSets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Notifications
{
    public partial class NotificationSetting : IAggregateRoot
    {
        public NotificationSetting()
        {
            this.Set = new List<NotificationSet>();
        }

        public NotificationSetting(NotificationEvent notificationEvent)
            : this()
        {
            this.NotificationEvent = notificationEvent;
            this.NotificationEventId = notificationEvent.NotificationEventId;
            this.Status = NotificationSettingStatus.Draft;
            this.CreateDate = DateTime.Now;
        }

        public int NotificationSettingId { get; set; }

        public int UserId { get; set; }

        public int NotificationEventId { get; set; }

        public NotificationSettingStatus Status { get; set; }

        public int? ProgrammeId { get; set; }

        public NotificationScope? Scope { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual NotificationEvent NotificationEvent { get; set; }

        public ICollection<NotificationSet> Set { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NotificationSettingMap : EntityTypeConfiguration<NotificationSetting>
    {
        public NotificationSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationSettingId);

            // Properties
            this.Property(t => t.NotificationSettingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.UserId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.NotificationEventId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("NotificationSettings");
            this.Property(t => t.NotificationSettingId).HasColumnName("NotificationSettingId");
            this.Property(t => t.NotificationEventId).HasColumnName("NotificationEventId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Scope).HasColumnName("Scope");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");

            this.HasRequired(t => t.NotificationEvent)
                .WithMany()
                .HasForeignKey(t => t.NotificationEventId);
        }
    }
}
