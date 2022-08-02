using System.Data.Entity.ModelConfiguration;

namespace Eumis.Portal.Model.Entities.Mapping
{
    public class BlobMap : EntityTypeConfiguration<Blob>
    {
        public BlobMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.Hash)
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("Blobs");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Hash).HasColumnName("Hash");
            this.Property(t => t.Size).HasColumnName("Size");
        }
    }
}
