using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractReportAttachedFinancialCorrection
    {

        public ContractReportAttachedFinancialCorrection()
        {
        }

        public int ContractReportId { get; set; }

        public int ContractReportFinancialCorrectionId { get; set; }

        public int ContractId { get; set; }

        public virtual ContractReport ContractReport { get; set; }
    }

    public class ContractReportAttachedFinancialCorrectionMap : EntityTypeConfiguration<ContractReportAttachedFinancialCorrection>
    {
        public ContractReportAttachedFinancialCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ContractReportId, t.ContractReportFinancialCorrectionId });

            // Properties
            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractReportFinancialCorrectionId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportAttachedFinancialCorrections");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractReportFinancialCorrectionId).HasColumnName("ContractReportFinancialCorrectionId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");

            //Relationships
            this.HasRequired(t => t.ContractReport)
                .WithMany(t => t.ContractReportAttachedFinancialCorrections)
                .HasForeignKey(t => t.ContractReportId)
                .WillCascadeOnDelete();
        }
    }
}
