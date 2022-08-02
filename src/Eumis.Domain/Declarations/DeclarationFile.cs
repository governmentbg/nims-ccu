using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain
{
    public class DeclarationFile
    {
        public int DeclarationFileId { get; set; }

        public int DeclarationId { get; set; }

        public Guid BlobKey { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Declaration Declaration { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class DeclarationFileMap : EntityTypeConfiguration<DeclarationFile>
    {
        public DeclarationFileMap()
        {
            // Primary Key
            this.HasKey(t => t.DeclarationFileId);

            // Properties
            this.Property(t => t.DeclarationFileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.DeclarationId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("DeclarationFiles");
            this.Property(t => t.DeclarationFileId).HasColumnName("DeclarationFileId");
            this.Property(t => t.DeclarationId).HasColumnName("DeclarationId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.Declaration)
                .WithMany(t => t.DeclarationFiles)
                .HasForeignKey(t => t.DeclarationId)
                .WillCascadeOnDelete();
        }
    }
}
