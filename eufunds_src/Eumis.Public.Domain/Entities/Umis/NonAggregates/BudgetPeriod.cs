using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
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
