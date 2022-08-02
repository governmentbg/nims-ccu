using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
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
