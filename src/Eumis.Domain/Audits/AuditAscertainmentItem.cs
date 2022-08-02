using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Audits
{
    public partial class AuditAscertainmentItem
    {
        public int AuditAscertainmentItemId { get; set; }

        public int AuditAscertainmentId { get; set; }

        public int AuditLevelItemId { get; set; }

        public virtual AuditAscertainment Ascertainment { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AuditAscertainmentItemMap : EntityTypeConfiguration<AuditAscertainmentItem>
    {
        public AuditAscertainmentItemMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditAscertainmentItemId);

            // Properties
            this.Property(t => t.AuditAscertainmentItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.AuditAscertainmentId)
                .IsRequired();
            this.Property(t => t.AuditLevelItemId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AuditAscertainmentItems");
            this.Property(t => t.AuditAscertainmentItemId).HasColumnName("AuditAscertainmentItemId");
            this.Property(t => t.AuditAscertainmentId).HasColumnName("AuditAscertainmentId");
            this.Property(t => t.AuditLevelItemId).HasColumnName("AuditLevelItemId");

            this.HasRequired(t => t.Ascertainment)
                .WithMany(t => t.Items)
                .HasForeignKey(t => t.AuditAscertainmentId)
                .WillCascadeOnDelete();
        }
    }
}
