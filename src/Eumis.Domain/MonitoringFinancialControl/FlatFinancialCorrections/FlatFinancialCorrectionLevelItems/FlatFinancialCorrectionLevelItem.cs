using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems
{
    public abstract class FlatFinancialCorrectionLevelItem
    {
        public FlatFinancialCorrectionLevelItem()
        {
        }

        public abstract FlatFinancialCorrectionLevel Type { get; }

        public int FlatFinancialCorrectionLevelItemId { get; set; }

        public int FlatFinancialCorrectionId { get; set; }

        public decimal? Percent { get; set; }

        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public virtual FlatFinancialCorrection FlatFinancialCorrection { get; set; }

        public void SetAttributes(
            decimal? percent,
            decimal? euAmount,
            decimal? bgAmount,
            decimal? totalAmount)
        {
            if (euAmount.HasValue && bgAmount.HasValue && totalAmount.HasValue && (euAmount.Value + bgAmount.Value != totalAmount.Value))
            {
                throw new DomainException("euAmount + bgAmount is not equal to totalAmount in FlatFinancialCorrection");
            }

            this.Percent = percent;
            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.TotalAmount = totalAmount;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class FlatFinancialCorrectionLevelItemMap : EntityTypeConfiguration<FlatFinancialCorrectionLevelItem>
    {
        public FlatFinancialCorrectionLevelItemMap()
        {
            // Primary Key
            this.HasKey(t => t.FlatFinancialCorrectionLevelItemId);

            // Properties
            this.Property(t => t.FlatFinancialCorrectionLevelItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Ignore(t => t.Type);

            // Table & Column Mappings
            this.ToTable("FlatFinancialCorrectionLevelItems");
            this.Property(t => t.FlatFinancialCorrectionLevelItemId).HasColumnName("FlatFinancialCorrectionLevelItemId");
            this.Property(t => t.FlatFinancialCorrectionId).HasColumnName("FlatFinancialCorrectionId");
            this.Property(t => t.Percent).HasColumnName("Percent");
            this.Property(t => t.EuAmount).HasColumnName("EuAmount");
            this.Property(t => t.BgAmount).HasColumnName("BgAmount");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");

            // Relationships
            this.HasRequired(t => t.FlatFinancialCorrection)
                .WithMany(t => t.FlatFinancialCorrectionLevelItems)
                .HasForeignKey(d => d.FlatFinancialCorrectionId)
                .WillCascadeOnDelete();

            // Derived entities
            this.Map<FlatFinancialCorrectionProgrammeItem>(t => t.Requires("Type").HasValue("Programme"));
            this.Map<FlatFinancialCorrectionProgrammePriorityItem>(t => t.Requires("Type").HasValue("ProgrammePriority"));
            this.Map<FlatFinancialCorrectionProcedureItem>(t => t.Requires("Type").HasValue("Procedure"));
            this.Map<FlatFinancialCorrectionContractItem>(t => t.Requires("Type").HasValue("Contract"));
            this.Map<FlatFinancialCorrectionContractContractItem>(t => t.Requires("Type").HasValue("ContractContract"));
        }
    }
}
