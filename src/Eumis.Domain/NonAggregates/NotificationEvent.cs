using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class NotificationEvent
    {
        public NotificationEvent()
        {
        }

        public int NotificationEventId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public bool IsProgrammeDependent { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NotificationEventMap : EntityTypeConfiguration<NotificationEvent>
    {
        public NotificationEventMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationEventId);

            // Properties
            this.Property(t => t.NotificationEventId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.NameAlt)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.IsProgrammeDependent)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("NotificationEvents");
            this.Property(t => t.NotificationEventId).HasColumnName("NotificationEventId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.IsProgrammeDependent).HasColumnName("IsProgrammeDependent");
        }
    }
}
