using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class RegulationInvestmentPriority
    {
        public static readonly string HiddenInvestmentPriorityCode = "00";

        public int InvestmentPriorityId { get; set; }

        public int InterventionCategoryId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class RegulationInvestmentPriorityMap : EntityTypeConfiguration<RegulationInvestmentPriority>
    {
        public RegulationInvestmentPriorityMap()
        {
            // Primary Key
            this.HasKey(t => t.InvestmentPriorityId);

            this.Property(t => t.InvestmentPriorityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.InterventionCategoryId)
                .IsRequired();
            this.Property(t => t.Code)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("InvestmentPriorities");
            this.Property(t => t.InvestmentPriorityId).HasColumnName("InvestmentPriorityId");
            this.Property(t => t.InterventionCategoryId).HasColumnName("InterventionCategoryId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
        }
    }
}
