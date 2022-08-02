using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularityFinancialStatus
    {
        public int IrregularityFinancialStatusId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularityFinancialStatusMap : EntityTypeConfiguration<IrregularityFinancialStatus>
    {
        public IrregularityFinancialStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularityFinancialStatusId);

            // Properties
            this.Property(t => t.IrregularityFinancialStatusId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Code)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("IrregularityFinancialStatuses");
            this.Property(t => t.IrregularityFinancialStatusId).HasColumnName("IrregularityFinancialStatusId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
