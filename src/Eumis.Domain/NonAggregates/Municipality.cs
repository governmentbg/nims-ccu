using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class Municipality
    {
        public Municipality()
        {
        }

        public int MunicipalityId { get; set; }

        public int DistrictId { get; set; }

        public string LauCode { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string FullPathName { get; set; }

        public string FullPath { get; set; }

        public virtual District District { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MunicipalityMap : EntityTypeConfiguration<Municipality>
    {
        public MunicipalityMap()
        {
            // Primary Key
            this.HasKey(t => t.MunicipalityId);

            // Properties
            this.Property(t => t.MunicipalityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.LauCode)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.DisplayName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FullPathName)
                .HasMaxLength(1000)
                .IsRequired();

            this.Property(t => t.FullPath)
                .HasMaxLength(500)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Municipalities");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.LauCode).HasColumnName("LauCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.FullPathName).HasColumnName("FullPathName");
            this.Property(t => t.FullPath).HasColumnName("FullPath");

            this.HasRequired(t => t.District)
                .WithMany()
                .HasForeignKey(t => t.DistrictId);
        }
    }
}
