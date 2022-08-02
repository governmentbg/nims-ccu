using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureMassCommunicationDocument
    {
        public int ProcedureMassCommunicationDocumentId { get; set; }

        public int ProcedureMassCommunicationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid? FileKey { get; set; }

        public virtual ProcedureMassCommunication Communication { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureMassCommunicationDocumentMap : EntityTypeConfiguration<ProcedureMassCommunicationDocument>
    {
        public ProcedureMassCommunicationDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureMassCommunicationDocumentId);

            // Properties
            this.Property(t => t.ProcedureMassCommunicationDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureMassCommunicationId)
                .IsRequired();

            this.Property(t => t.FileName)
                .IsOptional();

            this.Property(t => t.FileKey)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ProcedureMassCommunicationDocuments");
            this.Property(t => t.ProcedureMassCommunicationDocumentId).HasColumnName("ProcedureMassCommunicationDocumentId");
            this.Property(t => t.ProcedureMassCommunicationId).HasColumnName("ProcedureMassCommunicationId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.Communication)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.ProcedureMassCommunicationId)
                .WillCascadeOnDelete();
        }
    }
}
