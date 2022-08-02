using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.Programmes
{
    public class ProgrammeApplicationDocument
    {
        public ProgrammeApplicationDocument()
        {
        }

        public ProgrammeApplicationDocument(string name, string extension, bool isSignatureRequired)
        {
            this.Name = name;
            this.Extension = extension;
            this.IsSignatureRequired = isSignatureRequired;
            this.IsActive = true;
        }

        public int ProgrammeApplicationDocumentId { get; set; }

        public int ProgrammeId { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsSignatureRequired { get; set; }

        public bool IsActive { get; set; }

        public virtual Programme Programme { get; set; }

        internal void SetAttributes(string extension, bool isSignatureRequired)
        {
            this.Extension = extension;
            this.IsSignatureRequired = isSignatureRequired;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProgrammeApplicationDocumentMap : EntityTypeConfiguration<ProgrammeApplicationDocument>
    {
        public ProgrammeApplicationDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ProgrammeApplicationDocumentId);

            this.Property(t => t.ProgrammeApplicationDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Extension)
                .IsOptional()
                .HasMaxLength(100);

            this.Property(t => t.IsSignatureRequired)
                .IsRequired();

            this.Property(t => t.IsActive)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProgrammeApplicationDocuments");
            this.Property(t => t.ProgrammeApplicationDocumentId).HasColumnName("ProgrammeApplicationDocumentId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Extension).HasColumnName("Extension");
            this.Property(t => t.IsSignatureRequired).HasColumnName("IsSignatureRequired");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasRequired(t => t.Programme)
                .WithMany(t => t.ProgrammeApplicationDocuments)
                .HasForeignKey(t => t.ProgrammeId)
                .WillCascadeOnDelete();
        }
    }
}
