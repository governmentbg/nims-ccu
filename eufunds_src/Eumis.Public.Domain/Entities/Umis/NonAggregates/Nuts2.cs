using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class Nuts2
    {
        public Nuts2()
        {
        }

        public int Nuts2Id { get; set; }

        public int Nuts1Id { get; set; }

        public string NutsCode { get; set; }

        public string Name { get; set; }

        public string FullPathName { get; set; }

        public string NameAlt { get; set; }

        public string FullPathNameAlt { get; set; }

        public string FullPath { get; set; }

        public virtual Nuts1 Nuts1 { get; set; }
    }

    public class Nuts2Map : EntityTypeConfiguration<Nuts2>
    {
        public Nuts2Map()
        {
            // Primary Key
            this.HasKey(t => t.Nuts2Id);

            // Properties
            this.Property(t => t.Nuts2Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.NutsCode)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Name)
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
            this.ToTable("Nuts2s");
            this.Property(t => t.Nuts2Id).HasColumnName("Nuts2Id");
            this.Property(t => t.Nuts1Id).HasColumnName("Nuts1Id");
            this.Property(t => t.NutsCode).HasColumnName("NutsCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FullPathName).HasColumnName("FullPathName");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.FullPathNameAlt).HasColumnName("FullPathNameAlt");
            this.Property(t => t.FullPath).HasColumnName("FullPath");

            this.HasRequired(t => t.Nuts1)
                .WithMany()
                .HasForeignKey(t => t.Nuts1Id);
        }
    }
}
