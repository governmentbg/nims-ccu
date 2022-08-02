using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Audits
{
    public partial class AuditDoc
    {
        public int AuditDocId { get; set; }

        public int AuditId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual Audit Audit { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    public class AuditDocMap : EntityTypeConfiguration<AuditDoc>
    {
        public AuditDocMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditDocId);

            // Properties
            this.Property(t => t.AuditDocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.AuditId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AuditDocs");
            this.Property(t => t.AuditDocId).HasColumnName("AuditDocId");
            this.Property(t => t.AuditId).HasColumnName("AuditId");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.Audit)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.AuditId)
                .WillCascadeOnDelete();
        }
    }
}
