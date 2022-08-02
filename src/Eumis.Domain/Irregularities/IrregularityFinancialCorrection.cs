using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularityFinancialCorrection
    {
        public int IrregularityFinancialCorrectionId { get; set; }

        public int IrregularityId { get; set; }

        public int FinancialCorrectionId { get; set; }

        public virtual Irregularity Irregularity { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularityFinancialCorrectionMap : EntityTypeConfiguration<IrregularityFinancialCorrection>
    {
        public IrregularityFinancialCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularityFinancialCorrectionId);

            // Properties
            this.Property(t => t.IrregularityFinancialCorrectionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IrregularityId)
                .IsRequired();
            this.Property(t => t.FinancialCorrectionId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IrregularityFinancialCorrections");
            this.Property(t => t.IrregularityFinancialCorrectionId).HasColumnName("IrregularityFinancialCorrectionId");
            this.Property(t => t.IrregularityId).HasColumnName("IrregularityId");
            this.Property(t => t.FinancialCorrectionId).HasColumnName("FinancialCorrectionId");

            this.HasRequired(t => t.Irregularity)
                .WithMany(t => t.FinancialCorrections)
                .HasForeignKey(t => t.IrregularityId)
                .WillCascadeOnDelete();
        }
    }
}
