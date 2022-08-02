using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public partial class IrregularityDoc
    {
        public int IrregularityDocId { get; set; }

        public int IrregularityId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual Irregularity Irregularity { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    public class IrregularityDocMap : EntityTypeConfiguration<IrregularityDoc>
    {
        public IrregularityDocMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularityDocId);

            // Properties
            this.Property(t => t.IrregularityDocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IrregularityId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IrregularityDocs");
            this.Property(t => t.IrregularityDocId).HasColumnName("IrregularityDocId");
            this.Property(t => t.IrregularityId).HasColumnName("IrregularityId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.Irregularity)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.IrregularityId)
                .WillCascadeOnDelete();
        }
    }
}
