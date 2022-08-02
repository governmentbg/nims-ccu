using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.SpotChecks
{
    public partial class SpotCheckDoc
    {
        public int SpotCheckDocId { get; set; }

        public int SpotCheckId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual SpotCheck Check { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class SpotCheckDocMap : EntityTypeConfiguration<SpotCheckDoc>
    {
        public SpotCheckDocMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckDocId);

            // Properties
            this.Property(t => t.SpotCheckDocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SpotCheckId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SpotCheckDocs");
            this.Property(t => t.SpotCheckDocId).HasColumnName("SpotCheckDocId");
            this.Property(t => t.SpotCheckId).HasColumnName("SpotCheckId");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.Check)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.SpotCheckId)
                .WillCascadeOnDelete();
        }
    }
}
