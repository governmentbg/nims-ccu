using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes
{
    public class MapNodeDocument
    {
        public MapNodeDocument()
        {
        }

        public int MapNodeDocumentId { get; set; }
        public int MapNodeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? BlobKey { get; set; }

        public virtual Blob File { get; set; }
        public virtual MapNode MapNode { get; set; }

        internal void SetAttributes(
            string name,
            string description,
            Guid? blobKey)
        {
            this.Name = name;
            this.Description = description;
            this.BlobKey = blobKey;
        }
    }

    public class MapNodeDocumentMap : EntityTypeConfiguration<MapNodeDocument>
    {
        public MapNodeDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.MapNodeDocumentId);

            // Properties
            this.Property(t => t.MapNodeDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("MapNodeDocuments");
            this.Property(t => t.MapNodeDocumentId).HasColumnName("MapNodeDocumentId");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            // Relationships
            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(d => d.BlobKey);
            this.HasRequired(t => t.MapNode)
                .WithMany(t => t.MapNodeDocuments)
                .HasForeignKey(d => d.MapNodeId)
                .WillCascadeOnDelete();

        }
    }
}
