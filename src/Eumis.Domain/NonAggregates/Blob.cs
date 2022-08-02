using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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
