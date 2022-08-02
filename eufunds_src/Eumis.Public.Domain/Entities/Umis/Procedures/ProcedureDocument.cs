using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureDocument
    {
        public ProcedureDocument()
        {
        }

        public int ProcedureDocumentId { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? BlobKey { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual Blob File { get; set; }

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

    public class ProcedureDocumentMap : EntityTypeConfiguration<ProcedureDocument>
    {
        public ProcedureDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureDocumentId);

            // Properties
            this.Property(t => t.ProcedureDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ProcedureDocuments");
            this.Property(t => t.ProcedureDocumentId).HasColumnName("ProcedureDocumentId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureDocuments)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
