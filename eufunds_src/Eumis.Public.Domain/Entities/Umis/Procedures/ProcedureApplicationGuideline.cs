using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureApplicationGuideline
    {
        private ProcedureApplicationGuideline()
        {
        }

        public ProcedureApplicationGuideline(string name, string description, Guid blobKey)
        {
            this.Gid = Guid.NewGuid();
            this.SetAttributes(name, description, blobKey);
        }

        public int ProcedureApplicationGuidelineId { get; set; }

        public Guid Gid { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid BlobKey { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual Blob File { get; set; }

        internal void SetAttributes(
            string name,
            string description,
            Guid blobKey)
        {
            this.Name = name;
            this.Description = description;
            this.BlobKey = blobKey;
        }
    }

    public class ProcedureApplicationGuidelineMap : EntityTypeConfiguration<ProcedureApplicationGuideline>
    {
        public ProcedureApplicationGuidelineMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureApplicationGuidelineId);

            //Properties
            this.Property(t => t.ProcedureApplicationGuidelineId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureApplicationGuidelines");
            this.Property(t => t.ProcedureApplicationGuidelineId).HasColumnName("ProcedureApplicationGuidelineId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Decription");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureApplicationGuidelines)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
