using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionSheetXmlFile : RioXmlFile
    {
        private EvalSessionSheetXmlFile()
        {
        }

        public EvalSessionSheetXmlFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int EvalSessionSheetXmlId { get; set; }

        public EvalSessionSheetXmlFileType Type { get; set; }

        public virtual EvalSessionSheetXml EvalSheetXml { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EvalSessionSheetXmlFileMap : EntityTypeConfiguration<EvalSessionSheetXmlFile>
    {
        public EvalSessionSheetXmlFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.EvalSessionSheetXmlId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionSheetXmlFiles");
            this.Property(t => t.FileId).HasColumnName("EvalSessionSheetXmlFileId");
            this.Property(t => t.EvalSessionSheetXmlId).HasColumnName("EvalSessionSheetXmlId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.EvalSheetXml)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.EvalSessionSheetXmlId)
                .WillCascadeOnDelete();
        }
    }
}
