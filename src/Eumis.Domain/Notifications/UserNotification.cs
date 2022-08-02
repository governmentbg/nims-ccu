using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Notifications
{
    public partial class UserNotification : IAggregateRoot
    {
        public int UserNotificationId { get; set; }

        public int UserId { get; set; }

        public int NotificationEntryId { get; set; }

        public bool IsRead { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual NotificationEntry NotificationEntry { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class UserNotificationMap : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.UserNotificationId);

            // Properties
            this.Property(t => t.UserNotificationId)
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

            this.Property(t => t.NotificationEntryId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("UserNotifications");
            this.Property(t => t.UserNotificationId).HasColumnName("UserNotificationId");
            this.Property(t => t.NotificationEntryId).HasColumnName("NotificationEntryId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.IsRead).HasColumnName("IsRead");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");

            this.HasRequired(t => t.NotificationEntry)
                .WithMany()
                .HasForeignKey(t => t.NotificationEntryId);
        }
    }
}
