using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain
{
    public partial class Declaration : IAggregateRoot
    {
        private Declaration()
        {
            this.DeclarationFiles = new List<DeclarationFile>();
        }

        public Declaration(
            string name,
            string nameAlt,
            string content,
            string contentAlt,
            int createdByUserId,
            IList<DeclarationFile> declarationFiles)
            : this()
        {
            this.Status = DeclarationStatus.Draft;
            this.Name = name;
            this.NameAlt = nameAlt;
            this.Content = content;
            this.ContentAlt = contentAlt;
            this.CreatedByUserId = createdByUserId;
            this.DeclarationFiles = declarationFiles;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int DeclarationId { get; set; }

        public DeclarationStatus Status { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Content { get; set; }

        public string ContentAlt { get; set; }

        public DateTime? ActivationDate { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<DeclarationFile> DeclarationFiles { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class DeclarationMap : EntityTypeConfiguration<Declaration>
    {
        public DeclarationMap()
        {
            // Primary Key
            this.HasKey(t => t.DeclarationId);

            // Properties
            this.Property(t => t.DeclarationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.NameAlt)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.ContentAlt)
                .IsRequired();

            this.Property(t => t.CreatedByUserId)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Declarations");
            this.Property(t => t.DeclarationId).HasColumnName("DeclarationId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.ContentAlt).HasColumnName("ContentAlt");
            this.Property(t => t.ActivationDate).HasColumnName("ActivationDate");

            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
