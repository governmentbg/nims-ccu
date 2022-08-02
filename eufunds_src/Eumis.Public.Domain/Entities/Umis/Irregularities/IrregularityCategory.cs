using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public partial class IrregularityCategory
    {
        public int IrregularityCategoryId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }

    public class IrregularityCategoryMap : EntityTypeConfiguration<IrregularityCategory>
    {
        public IrregularityCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularityCategoryId);

            // Properties
            this.Property(t => t.IrregularityCategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Code)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("IrregularityCategories");
            this.Property(t => t.IrregularityCategoryId).HasColumnName("IrregularityCategoryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
