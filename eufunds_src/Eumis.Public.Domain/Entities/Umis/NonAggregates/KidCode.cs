using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class KidCode
    {
        public KidCode()
        {
        }

        public int KidCodeId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }

    public class KidCodeMap : EntityTypeConfiguration<KidCode>
    {
        public KidCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.KidCodeId);

            // Properties
            this.Property(t => t.KidCodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("KidCodes");
            this.Property(t => t.KidCodeId).HasColumnName("KidCodeId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
