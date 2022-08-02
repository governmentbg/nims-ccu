using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.SpotChecks
{
    public partial class SpotCheckPlanDoc
    {
        public int SpotCheckPlanDocId { get; set; }

        public int SpotCheckPlanId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual SpotCheckPlan Plan { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class SpotCheckPlanDocMap : EntityTypeConfiguration<SpotCheckPlanDoc>
    {
        public SpotCheckPlanDocMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckPlanDocId);

            // Properties
            this.Property(t => t.SpotCheckPlanDocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SpotCheckPlanId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SpotCheckPlanDocs");
            this.Property(t => t.SpotCheckPlanDocId).HasColumnName("SpotCheckPlanDocId");
            this.Property(t => t.SpotCheckPlanId).HasColumnName("SpotCheckPlanId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.Plan)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.SpotCheckPlanId)
                .WillCascadeOnDelete();
        }
    }
}
