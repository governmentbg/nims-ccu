using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Allowances
{
    public partial class AllowanceRate
    {
        public int AllowanceRateId { get; set; }

        public int AllowanceId { get; set; }

        public DateTime Date { get; set; }

        public decimal Rate { get; set; }

        public virtual Allowance Allowance { get; set; }

        internal void SetAttributes(decimal rate)
        {
            this.Rate = rate;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AllowanceRateMap : EntityTypeConfiguration<AllowanceRate>
    {
        public AllowanceRateMap()
        {
            // Primary Key
            this.HasKey(t => t.AllowanceRateId);

            // Properties
            this.Property(t => t.AllowanceRateId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Date)
                .IsRequired();

            this.Property(t => t.Rate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AllowanceRates");
            this.Property(t => t.AllowanceRateId).HasColumnName("AllowanceRateId");
            this.Property(t => t.AllowanceId).HasColumnName("AllowanceId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Rate).HasColumnName("Rate");

            // Relationships
            this.HasRequired(t => t.Allowance)
                .WithMany(t => t.Rates)
                .HasForeignKey(d => d.AllowanceId)
                .WillCascadeOnDelete();
        }
    }
}
