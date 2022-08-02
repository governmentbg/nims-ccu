using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public class ProcedureQuestion
    {
        public ProcedureQuestion()
        {
        }

        public int ProcedureQuestionId { get; set; }

        public int ProcedureId { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid BlobKey { get; set; }

        public bool IsActivated { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual Blob File { get; set; }

        internal void SetAttributes(
            Guid blobKey)
        {
            this.BlobKey = blobKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureQuestionMap : EntityTypeConfiguration<ProcedureQuestion>
    {
        public ProcedureQuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureQuestionId);

            // Properties
            this.Property(t => t.ProcedureQuestionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ProcedureQuestions");
            this.Property(t => t.ProcedureQuestionId).HasColumnName("ProcedureQuestionId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureQuestions)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
