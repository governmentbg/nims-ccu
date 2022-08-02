using System.Data.Entity.ModelConfiguration;

namespace Eumis.Portal.Model.Entities.Mapping
{
    public class ProtectedZoneMap : EntityTypeConfiguration<ProtectedZone>
    {
        public ProtectedZoneMap()
        {
            // Primary Key
            this.HasKey(t => t.ProtectedZoneId);

            // Properties
            this.Property(t => t.NutsCode)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.FullPathName)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.FullPath)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ProtectedZones");
            this.Property(t => t.ProtectedZoneId).HasColumnName("ProtectedZoneId");
            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.NutsCode).HasColumnName("NutsCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.FullPathName).HasColumnName("FullPathName");
            this.Property(t => t.FullPathNameAlt).HasColumnName("FullPathNameAlt");
            this.Property(t => t.FullPath).HasColumnName("FullPath");

            // Relationships
            this.HasRequired(t => t.Country)
                .WithMany(t => t.ProtectedZones)
                .HasForeignKey(t => t.CountryId);
        }
    }
}
