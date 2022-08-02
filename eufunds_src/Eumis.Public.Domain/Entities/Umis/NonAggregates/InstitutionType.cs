using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class InstitutionType
    {
        public InstitutionType()
        {
        }

        public int InstitutionTypeId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }
    }

    public class InstitutionTypeMap : EntityTypeConfiguration<InstitutionType>
    {
        public InstitutionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.InstitutionTypeId);

            // Properties
            this.Property(t => t.InstitutionTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("InstitutionTypes");
            this.Property(t => t.InstitutionTypeId).HasColumnName("InstitutionTypeId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");

        }
    }
}
