using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class ErrandType
    {
        public ErrandType()
        {
        }

        public int ErrandTypeId { get; set; }

        public int ErrandLegalActId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ErrandTypeMap : EntityTypeConfiguration<ErrandType>
    {
        public ErrandTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ErrandTypeId);

            // Properties
            this.Property(t => t.ErrandTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ErrandTypes");
            this.Property(t => t.ErrandTypeId).HasColumnName("ErrandTypeId");
            this.Property(t => t.ErrandLegalActId).HasColumnName("ErrandLegalActId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
