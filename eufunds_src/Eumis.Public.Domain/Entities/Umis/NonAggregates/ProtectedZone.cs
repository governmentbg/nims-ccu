using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class ProtectedZone
    {
        public ProtectedZone()
        {
        }

        public int ProtectedZoneId { get; set; }

        public int CountryId { get; set; }

        public string NutsCode { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string FullPathName { get; set; }

        public string FullPathNameAlt { get; set; }

        public string FullPath { get; set; }

        public virtual Country Country { get; set; }
    }

    public class ProtectedZoneMap : EntityTypeConfiguration<ProtectedZone>
    {
        public ProtectedZoneMap()
        {
            // Primary Key
            this.HasKey(t => t.ProtectedZoneId);

            // Properties
            this.Property(t => t.ProtectedZoneId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.NutsCode)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.NameAlt)
                .HasMaxLength(200);

            this.Property(t => t.FullPathName)
                .HasMaxLength(1000)
                .IsRequired();

            this.Property(t => t.FullPathNameAlt)
                .HasMaxLength(1000);

            this.Property(t => t.FullPath)
                .HasMaxLength(500)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProtectedZones");
            this.Property(t => t.ProtectedZoneId).HasColumnName("ProtectedZoneId");
            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.NutsCode).HasColumnName("NutsCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FullPathName).HasColumnName("FullPathName");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.FullPathNameAlt).HasColumnName("FullPathNameAlt");
            this.Property(t => t.FullPath).HasColumnName("FullPath");

            this.HasRequired(t => t.Country)
                .WithMany()
                .HasForeignKey(t => t.CountryId);
        }
    }
}
