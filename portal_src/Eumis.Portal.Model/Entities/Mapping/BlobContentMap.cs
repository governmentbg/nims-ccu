using System.Data.Entity.ModelConfiguration;

namespace Eumis.Portal.Model.Entities.Mapping
{
    public class BlobContentMap : EntityTypeConfiguration<BlobContent>
    {
        public BlobContentMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.Hash)
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("BlobContents");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Hash).HasColumnName("Hash");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
