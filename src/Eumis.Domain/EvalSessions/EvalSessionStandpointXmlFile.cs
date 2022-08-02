using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionStandpointXmlFile : RioXmlFile
    {
        private EvalSessionStandpointXmlFile()
        {
        }

        public EvalSessionStandpointXmlFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int EvalSessionStandpointXmlId { get; set; }

        public virtual EvalSessionStandpointXml StandpointXml { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EvalSessionStandpointXmlFileMap : EntityTypeConfiguration<EvalSessionStandpointXmlFile>
    {
        public EvalSessionStandpointXmlFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.EvalSessionStandpointXmlId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionStandpointXmlFiles");
            this.Property(t => t.FileId).HasColumnName("EvalSessionStandpointXmlFileId");
            this.Property(t => t.EvalSessionStandpointXmlId).HasColumnName("EvalSessionStandpointXmlId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.StandpointXml)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.EvalSessionStandpointXmlId)
                .WillCascadeOnDelete();
        }
    }
}
