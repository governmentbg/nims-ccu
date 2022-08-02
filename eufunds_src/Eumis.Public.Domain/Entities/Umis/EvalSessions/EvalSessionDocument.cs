using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionDocument
    {
        public EvalSessionDocument()
        {
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionDocumentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid BlobKey { get; set; }

        public virtual EvalSession EvalSession { get; set; }

        public virtual Blob File { get; set; }

        internal void SetAttributes(
            string name,
            Guid blobKey,
            string description)
        {
            this.Name = name;
            this.BlobKey = blobKey;
            this.Description = description;
        }
    }

    public class EvalSessionDocumentMap : EntityTypeConfiguration<EvalSessionDocument>
    {
        public EvalSessionDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.EvalSessionDocumentId);

            // Properties
            this.Property(t => t.EvalSessionDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionDocuments");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionDocumentId).HasColumnName("EvalSessionDocumentId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            //Relationships
            this.HasRequired(t => t.EvalSession)
                .WithMany(t => t.EvalSessionDocuments)
                .HasForeignKey(t => t.EvalSessionId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.File)
                .WithMany()
                .HasForeignKey(d => d.BlobKey);
        }
    }
}
