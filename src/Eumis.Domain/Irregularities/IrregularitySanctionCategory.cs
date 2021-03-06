using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularitySanctionCategory
    {
        public int IrregularitySanctionCategoryId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularitySanctionCategoryMap : EntityTypeConfiguration<IrregularitySanctionCategory>
    {
        public IrregularitySanctionCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularitySanctionCategoryId);

            // Properties
            this.Property(t => t.IrregularitySanctionCategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Code)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("IrregularitySanctionCategories");
            this.Property(t => t.IrregularitySanctionCategoryId).HasColumnName("IrregularitySanctionCategoryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
