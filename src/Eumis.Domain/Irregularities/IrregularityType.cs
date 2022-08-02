using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularityType
    {
        public int IrregularityTypeId { get; set; }

        public int IrregularityCategoryId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularityTypeMap : EntityTypeConfiguration<IrregularityType>
    {
        public IrregularityTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularityTypeId);

            // Properties
            this.Property(t => t.IrregularityTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IrregularityCategoryId)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Code)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("IrregularityTypes");
            this.Property(t => t.IrregularityTypeId).HasColumnName("IrregularityTypeId");
            this.Property(t => t.IrregularityCategoryId).HasColumnName("IrregularityCategoryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
