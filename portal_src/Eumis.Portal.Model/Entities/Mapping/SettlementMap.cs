using System.Data.Entity.ModelConfiguration;

namespace Eumis.Portal.Model.Entities.Mapping
{
    public class SettlementMap : EntityTypeConfiguration<Settlement>
    {
        public SettlementMap()
        {
            // Primary Key
            this.HasKey(t => t.SettlementId);

            // Properties
            this.Property(t => t.LauCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.FullPathName)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.FullPath)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Order)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Settlements");
            this.Property(t => t.SettlementId).HasColumnName("SettlementId");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");
            this.Property(t => t.LauCode).HasColumnName("LauCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.FullPathName).HasColumnName("FullPathName");
            this.Property(t => t.FullPathNameAlt).HasColumnName("FullPathNameAlt");
            this.Property(t => t.FullPath).HasColumnName("FullPath");
            this.Property(t => t.Order).HasColumnName("Order");

            // Relationships
            this.HasRequired(t => t.Municipality)
                .WithMany(t => t.Settlements)
                .HasForeignKey(d => d.MunicipalityId);

        }
    }
}
