using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class District
    {
        public District()
        {
        }

        public int DistrictId { get; set; }

        public int Nuts2Id { get; set; }

        public string NutsCode { get; set; }

        public string Name { get; set; }

        public string FullPathName { get; set; }

        public string NameAlt { get; set; }

        public string FullPathNameAlt { get; set; }

        public string FullPath { get; set; }

        public virtual Nuts2 Nuts2 { get; set; }
    }

    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            // Primary Key
            this.HasKey(t => t.DistrictId);

            // Properties
            this.Property(t => t.DistrictId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.NutsCode)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FullPathName)
                .HasMaxLength(1000)
                .IsRequired();

            this.Property(t => t.NameAlt)
                .HasMaxLength(200);

            this.Property(t => t.FullPathNameAlt)
                .HasMaxLength(1000);

            this.Property(t => t.FullPath)
                .HasMaxLength(500)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Districts");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.Nuts2Id).HasColumnName("Nuts2Id");
            this.Property(t => t.NutsCode).HasColumnName("NutsCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FullPathName).HasColumnName("FullPathName");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.FullPathNameAlt).HasColumnName("FullPathNameAlt");
            this.Property(t => t.FullPath).HasColumnName("FullPath");

            this.HasRequired(t => t.Nuts2)
                .WithMany()
                .HasForeignKey(t => t.Nuts2Id);
        }
    }
}
