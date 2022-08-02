using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularityVersionDoc
    {
        public int IrregularityVersionDocId { get; set; }

        public int IrregularityVersionId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual IrregularityVersion Version { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularityVersionDocMap : EntityTypeConfiguration<IrregularityVersionDoc>
    {
        public IrregularityVersionDocMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularityVersionDocId);

            // Properties
            this.Property(t => t.IrregularityVersionDocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IrregularityVersionId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IrregularityVersionDocs");
            this.Property(t => t.IrregularityVersionDocId).HasColumnName("IrregularityVersionDocId");
            this.Property(t => t.IrregularityVersionId).HasColumnName("IrregularityVersionId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.Version)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.IrregularityVersionId)
                .WillCascadeOnDelete();
        }
    }
}
