using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.BasicInterestRates
{
    public partial class InterestRate
    {
        public int InterestRateId { get; set; }

        public int BasicInterestRateId { get; set; }

        public DateTime Date { get; set; }

        public decimal Rate { get; set; }

        public virtual BasicInterestRate BasicInterestRate { get; set; }

        internal void SetAttributes(decimal rate)
        {
            this.Rate = rate;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class InterestRateMap : EntityTypeConfiguration<InterestRate>
    {
        public InterestRateMap()
        {
            // Primary Key
            this.HasKey(t => t.InterestRateId);

            // Properties
            this.Property(t => t.InterestRateId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Date)
                .IsRequired();

            this.Property(t => t.Rate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("InterestRates");
            this.Property(t => t.InterestRateId).HasColumnName("InterestRateId");
            this.Property(t => t.BasicInterestRateId).HasColumnName("BasicInterestRateId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Rate).HasColumnName("Rate");

            // Relationships
            this.HasRequired(t => t.BasicInterestRate)
                .WithMany(t => t.Rates)
                .HasForeignKey(d => d.BasicInterestRateId)
                .WillCascadeOnDelete();
        }
    }
}
