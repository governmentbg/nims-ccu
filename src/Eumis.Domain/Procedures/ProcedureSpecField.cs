using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public class ProcedureSpecField
    {
        private ProcedureSpecField()
        {
        }

        public ProcedureSpecField(string title, string titleAlt, string description, string descriptionAlt, bool isRequired, ProcedureSpecFieldMaxLength maxLength)
        {
            this.Gid = Guid.NewGuid();
            this.SetAttributes(title, titleAlt, description, descriptionAlt, isRequired, maxLength);

            this.IsActivated = false;
            this.IsActive = true;
        }

        public int ProcedureSpecFieldId { get; set; }

        public Guid Gid { get; set; }

        public int ProcedureId { get; set; }

        public string Title { get; set; }

        public string TitleAlt { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public bool IsRequired { get; set; }

        public ProcedureSpecFieldMaxLength MaxLength { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public virtual Procedure Procedure { get; set; }

        internal void SetAttributes(string title, string titleAlt, string description, string descriptionAlt, bool isRequired, ProcedureSpecFieldMaxLength maxLength)
        {
            this.Title = title;
            this.TitleAlt = string.IsNullOrEmpty(titleAlt) ? title : titleAlt;
            this.Description = description;
            this.DescriptionAlt = string.IsNullOrEmpty(descriptionAlt) ? description : descriptionAlt;
            this.IsRequired = isRequired;
            this.MaxLength = maxLength;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureSpecFieldMap : EntityTypeConfiguration<ProcedureSpecField>
    {
        public ProcedureSpecFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureSpecFieldId);

            // Properties
            this.Property(t => t.ProcedureSpecFieldId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ProcedureSpecFields");
            this.Property(t => t.ProcedureSpecFieldId).HasColumnName("ProcedureSpecFieldId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TitleAlt).HasColumnName("TitleAlt");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionAlt).HasColumnName("DescriptionAlt");
            this.Property(t => t.IsRequired).HasColumnName("IsRequired");
            this.Property(t => t.MaxLength).HasColumnName("MaxLength");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureSpecFields)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
