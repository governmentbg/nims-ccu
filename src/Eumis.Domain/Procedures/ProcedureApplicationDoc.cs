using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public class ProcedureApplicationDoc
    {
        private ProcedureApplicationDoc()
        {
        }

        public ProcedureApplicationDoc(
            int? programmeApplicationDocumentId,
            string name,
            bool isRequired,
            bool isSignatureRequired)
        {
            this.Gid = Guid.NewGuid();
            this.ProgrammeApplicationDocumentId = programmeApplicationDocumentId;
            this.Name = name;
            this.IsRequired = isRequired;
            this.IsSignatureRequired = isSignatureRequired;

            this.IsActivated = false;
            this.IsActive = true;
        }

        public int ProcedureApplicationDocId { get; set; }

        public Guid Gid { get; set; }

        public int ProcedureId { get; set; }

        public int? ProgrammeApplicationDocumentId { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsRequired { get; set; }

        public bool IsSignatureRequired { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public virtual Procedure Procedure { get; set; }

        internal void SetAttributes(
            int? programmeApplicationDocumentId,
            string name,
            string extension,
            bool isRequired,
            bool isSignatureRequired)
        {
            this.ProgrammeApplicationDocumentId = programmeApplicationDocumentId;
            this.Name = name;
            this.Extension = extension;
            this.IsRequired = isRequired;
            this.IsSignatureRequired = isSignatureRequired;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureApplicationDocMap : EntityTypeConfiguration<ProcedureApplicationDoc>
    {
        public ProcedureApplicationDocMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureApplicationDocId);

            this.Property(t => t.ProcedureApplicationDocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Extension)
                .IsOptional()
                .HasMaxLength(100);

            this.Property(t => t.IsRequired)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureApplicationDocs");
            this.Property(t => t.ProcedureApplicationDocId).HasColumnName("ProcedureApplicationDocId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProgrammeApplicationDocumentId).HasColumnName("ProgrammeApplicationDocumentId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Extension).HasColumnName("Extension");
            this.Property(t => t.IsRequired).HasColumnName("IsRequired");
            this.Property(t => t.IsSignatureRequired).HasColumnName("IsSignatureRequired");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureApplicationDocs)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
