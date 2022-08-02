using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Portal.Model.Entities.Mapping
{
    public class CompanySizeTypeMap : EntityTypeConfiguration<CompanySizeType>
    {
        public CompanySizeTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanySizeTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CompanySizeTypes");
            this.Property(t => t.CompanySizeTypeId).HasColumnName("CompanySizeTypeId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Order).HasColumnName("Order");
            this.Property(t => t.Alias).HasColumnName("Alias");
        }
    }
}
