using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public partial class SpotCheckAscertainmentItem
    {
        public int SpotCheckAscertainmentItemId { get; set; }

        public int SpotCheckAscertainmentId { get; set; }

        public int SpotCheckItemId { get; set; }

        public virtual SpotCheckAscertainment Ascertainment { get; set; }
    }

    public class SpotCheckAscertainmentItemMap : EntityTypeConfiguration<SpotCheckAscertainmentItem>
    {
        public SpotCheckAscertainmentItemMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckAscertainmentItemId);

            // Properties
            this.Property(t => t.SpotCheckAscertainmentItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SpotCheckAscertainmentId)
                .IsRequired();
            this.Property(t => t.SpotCheckItemId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SpotCheckAscertainmentItems");
            this.Property(t => t.SpotCheckAscertainmentItemId).HasColumnName("SpotCheckAscertainmentItemId");
            this.Property(t => t.SpotCheckAscertainmentId).HasColumnName("SpotCheckAscertainmentId");
            this.Property(t => t.SpotCheckItemId).HasColumnName("SpotCheckItemId");

            this.HasRequired(t => t.Ascertainment)
                .WithMany(t => t.Items)
                .HasForeignKey(t => t.SpotCheckAscertainmentId)
                .WillCascadeOnDelete();
        }
    }
}
