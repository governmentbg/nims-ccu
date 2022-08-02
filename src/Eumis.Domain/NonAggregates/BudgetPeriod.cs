using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class BudgetPeriod
    {
        public BudgetPeriod()
        {
        }

        public int BudgetPeriodId { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class BudgetPeriodMap : EntityTypeConfiguration<BudgetPeriod>
    {
        public BudgetPeriodMap()
        {
            // Primary Key
            this.HasKey(t => t.BudgetPeriodId);

            // Properties
            this.Property(t => t.BudgetPeriodId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.Year)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("BudgetPeriods");
            this.Property(t => t.BudgetPeriodId).HasColumnName("BudgetPeriodId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Year).HasColumnName("Year");
        }
    }
}
