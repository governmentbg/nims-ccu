using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularitySanctionType
    {
        public int IrregularitySanctionTypeId { get; set; }

        public int IrregularitySanctionCategoryId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularitySanctionTypeMap : EntityTypeConfiguration<IrregularitySanctionType>
    {
        public IrregularitySanctionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularitySanctionTypeId);

            // Properties
            this.Property(t => t.IrregularitySanctionTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IrregularitySanctionCategoryId)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Code)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("IrregularitySanctionTypes");
            this.Property(t => t.IrregularitySanctionTypeId).HasColumnName("IrregularitySanctionTypeId");
            this.Property(t => t.IrregularitySanctionCategoryId).HasColumnName("IrregularitySanctionCategoryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
