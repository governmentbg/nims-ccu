using System.Data.Entity.ModelConfiguration;

namespace Eumis.Portal.Model.Entities.Mapping
{
    public class MunicipalityMap : EntityTypeConfiguration<Municipality>
    {
        public MunicipalityMap()
        {
            // Primary Key
            this.HasKey(t => t.MunicipalityId);

            // Properties
            this.Property(t => t.LauCode)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.FullPathName)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.FullPath)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Municipalities");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.LauCode).HasColumnName("LauCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.FullPathName).HasColumnName("FullPathName");
            this.Property(t => t.FullPathNameAlt).HasColumnName("FullPathNameAlt");
            this.Property(t => t.FullPath).HasColumnName("FullPath");

            // Relationships
            this.HasRequired(t => t.District)
                .WithMany(t => t.Municipalities)
                .HasForeignKey(d => d.DistrictId);

        }
    }
}
