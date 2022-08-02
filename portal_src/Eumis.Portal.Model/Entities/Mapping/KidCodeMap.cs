using System.Data.Entity.ModelConfiguration;

namespace Eumis.Portal.Model.Entities.Mapping
{
    public class KidCodeMap : EntityTypeConfiguration<KidCode>
    {
        public KidCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.KidCodeId);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("KidCodes");
            this.Property(t => t.KidCodeId).HasColumnName("KidCodeId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
        }
    }
}
