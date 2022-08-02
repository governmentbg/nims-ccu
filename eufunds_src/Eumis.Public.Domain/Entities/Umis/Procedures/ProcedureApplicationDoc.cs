using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureApplicationDoc
    {
        private ProcedureApplicationDoc()
        {
        }

        public ProcedureApplicationDoc(
            string name,
            string extension,
            bool isRequired,
            bool isSignatureRequired)
        {
            this.Gid = Guid.NewGuid();
            this.Name = name;
            this.Extension = extension;
            this.IsRequired = isRequired;
            this.IsSignatureRequired = isSignatureRequired;

            this.IsActivated = false;
            this.IsActive = true;
        }

        public int ProcedureApplicationDocId { get; set; }

        public Guid Gid { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsRequired { get; set; }

        public bool IsSignatureRequired { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public virtual Procedure Procedure { get; set; }

        internal void SetAttributes(
            string name,
            string extension,
            bool isRequired,
            bool isSignatureRequired)
        {
            this.Name = name;
            this.Extension = extension;
            this.IsRequired = isRequired;
            this.IsSignatureRequired = isSignatureRequired;
        }
    }

    public class ProcedureApplicationDocMap : EntityTypeConfiguration<ProcedureApplicationDoc>
    {
        public ProcedureApplicationDocMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureApplicationDocId);

            this.Property(t => t.ProcedureApplicationDocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Extension)
                .IsOptional()
                .HasMaxLength(50);

            this.Property(t => t.IsRequired)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureApplicationDocs");
            this.Property(t => t.ProcedureApplicationDocId).HasColumnName("ProcedureApplicationDocId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Extension).HasColumnName("Extension");
            this.Property(t => t.IsRequired).HasColumnName("IsRequired");
            this.Property(t => t.IsSignatureRequired).HasColumnName("IsSignatureRequired");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureApplicationDocs)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
