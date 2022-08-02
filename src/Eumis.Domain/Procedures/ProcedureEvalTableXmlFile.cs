using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Procedures
{
    public class ProcedureEvalTableXmlFile : RioXmlFile
    {
        private ProcedureEvalTableXmlFile()
        {
        }

        public ProcedureEvalTableXmlFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int ProcedureEvalTableXmlId { get; set; }

        public virtual ProcedureEvalTableXml EvalTableXml { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureEvalTableXmlFileMap : EntityTypeConfiguration<ProcedureEvalTableXmlFile>
    {
        public ProcedureEvalTableXmlFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureEvalTableXmlId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureEvalTableXmlFiles");
            this.Property(t => t.FileId).HasColumnName("ProcedureEvalTableXmlFileId");
            this.Property(t => t.ProcedureEvalTableXmlId).HasColumnName("ProcedureEvalTableXmlId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.EvalTableXml)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.ProcedureEvalTableXmlId)
                .WillCascadeOnDelete();
        }
    }
}
