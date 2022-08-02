using System.Data.Entity.ModelConfiguration;

namespace Eumis.Portal.Model.Entities.Mapping
{
    public class GParamMap : EntityTypeConfiguration<GParam>
    {
        public GParamMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.Key)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Value)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("GParams");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Value).HasColumnName("Value");
        }
    }
}
