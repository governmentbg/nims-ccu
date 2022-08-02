using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.ProgrammeDeclarations
{
    public abstract partial class ProgrammeDeclaration : IAggregateRoot
    {
        protected ProgrammeDeclaration()
        {
            this.ProgrammeDeclarationItems = new List<ProgrammeDeclarationItem>();
        }

        protected ProgrammeDeclaration(
            int programmeId,
            string name,
            string nameAlt,
            string content,
            string contentAlt,
            FieldType fieldType,
            int orderNum,
            bool isConsentForNSIDataProviding)
            : this()
        {
            this.ProgrammeId = programmeId;
            this.Name = name;
            this.NameAlt = nameAlt;
            this.Content = content;
            this.ContentAlt = contentAlt;
            this.FieldType = fieldType;
            this.OrderNum = orderNum;
            this.IsConsentForNSIDataProviding = isConsentForNSIDataProviding;
            this.IsActive = true;
        }

        public int ProgrammeDeclarationId { get; set; }

        public int ProgrammeId { get; set; }

        public int OrderNum { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Content { get; set; }

        public string ContentAlt { get; set; }

        public bool IsActive { get; set; }

        public FieldType FieldType { get; set; }

        public bool IsConsentForNSIDataProviding { get; set; }

        public abstract ProgrammeDeclarationType Type { get; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual ICollection<ProgrammeDeclarationItem> ProgrammeDeclarationItems { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProgrammeDeclarationMap : EntityTypeConfiguration<ProgrammeDeclaration>
    {
        public ProgrammeDeclarationMap()
        {
            // Primary Key
            this.HasKey(t => t.ProgrammeDeclarationId);

            this.Property(t => t.ProgrammeDeclarationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.NameAlt)
                .IsOptional()
                .HasMaxLength(500);

            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.ContentAlt)
                .IsOptional();

            this.Property(t => t.IsActive)
                .IsRequired();

            this.Property(t => t.FieldType)
                .IsRequired();

            this.Property(t => t.IsConsentForNSIDataProviding)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProgrammeDeclarations");
            this.Property(t => t.ProgrammeDeclarationId).HasColumnName("ProgrammeDeclarationId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.ContentAlt).HasColumnName("ContentAlt");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.FieldType).HasColumnName("FieldType");
            this.Property(t => t.IsConsentForNSIDataProviding).HasColumnName("IsConsentForNSIDataProviding");

            this.Map<ProgrammeAppFormDeclaration>(t => t.Requires("Type").HasValue<int>((int)ProgrammeDeclarationType.AppForm));
        }
    }
}
