using Eumis.Domain.NonAggregates;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Notifications
{
    public class NotificationEventPermission
    {
        public int NotificationEventPermissionId { get; set; }

        public int NotificationEventId { get; set; }

        public string Permission { get; set; }

        public string PermissionType { get; set; }

        public virtual NotificationEvent NotificationEvent { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NotificationEventPermissionMap : EntityTypeConfiguration<NotificationEventPermission>
    {
        public NotificationEventPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationEventPermissionId);

            // Properties
            this.Property(t => t.NotificationEventPermissionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Permission)
                .IsRequired()
                .HasMaxLength(100);

            // Properties
            this.Property(t => t.PermissionType)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("NotificationEventPermissions");
            this.Property(t => t.NotificationEventPermissionId).HasColumnName("NotificationEventPermissionId");
            this.Property(t => t.NotificationEventId).HasColumnName("NotificationEventId");
            this.Property(t => t.Permission).HasColumnName("Permission");
            this.Property(t => t.PermissionType).HasColumnName("PermissionType");

            // Relationships
            this.HasRequired(t => t.NotificationEvent)
                .WithMany()
                .HasForeignKey(t => t.NotificationEventId);
        }
    }
}
