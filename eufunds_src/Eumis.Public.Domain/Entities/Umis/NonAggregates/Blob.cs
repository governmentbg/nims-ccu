using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class Blob
    {
        public Blob()
        {
        }

        public Guid Key { get; set; }

        public string FileName { get; set; }

        public long BlobContentLocationId { get; set; }

    }

    public class BlobMap : EntityTypeConfiguration<Blob>
    {
        public BlobMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Table & Column Mappings
            this.ToTable("Blobs");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.BlobContentLocationId).HasColumnName("BlobContentLocationId");

        }
    }
}
