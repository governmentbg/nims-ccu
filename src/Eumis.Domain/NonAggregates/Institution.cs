using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class Institution
    {
        public Institution()
        {
        }

        public int InstitutionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class InstitutionMap : EntityTypeConfiguration<Institution>
    {
        public InstitutionMap()
        {
            // Primary Key
            this.HasKey(t => t.InstitutionId);

            // Properties
            this.Property(t => t.InstitutionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Description)
                .HasMaxLength(50)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Institutions");
            this.Property(t => t.InstitutionId).HasColumnName("InstitutionId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
