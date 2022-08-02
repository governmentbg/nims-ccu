using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularitySignalDoc
    {
        public int IrregularitySignalDocId { get; set; }

        public int IrregularitySignalId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual IrregularitySignal IrregularitySignal { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularitySignalDocMap : EntityTypeConfiguration<IrregularitySignalDoc>
    {
        public IrregularitySignalDocMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularitySignalDocId);

            // Properties
            this.Property(t => t.IrregularitySignalDocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IrregularitySignalId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IrregularitySignalDocs");
            this.Property(t => t.IrregularitySignalDocId).HasColumnName("IrregularitySignalDocId");
            this.Property(t => t.IrregularitySignalId).HasColumnName("IrregularitySignalId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.IrregularitySignal)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.IrregularitySignalId)
                .WillCascadeOnDelete();
        }
    }
}
