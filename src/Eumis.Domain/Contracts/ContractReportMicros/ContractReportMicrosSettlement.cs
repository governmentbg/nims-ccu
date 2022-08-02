using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public partial class ContractReportMicrosSettlement
    {
        public int ContractReportMicrosSettlementId { get; set; }

        public int ContractReportMicrosMunicipalityId { get; set; }

        public int SettlementId { get; set; }

        public string Name { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportMicrosSettlementMap : EntityTypeConfiguration<ContractReportMicrosSettlement>
    {
        public ContractReportMicrosSettlementMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportMicrosSettlementId);

            // Properties
            this.Property(t => t.ContractReportMicrosSettlementId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.ContractReportMicrosMunicipalityId)
                .IsRequired();
            this.Property(t => t.SettlementId)
                .IsRequired();
            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportMicrosSettlements");
            this.Property(t => t.ContractReportMicrosSettlementId).HasColumnName("ContractReportMicrosSettlementId");
            this.Property(t => t.ContractReportMicrosMunicipalityId).HasColumnName("ContractReportMicrosMunicipalityId");
            this.Property(t => t.SettlementId).HasColumnName("SettlementId");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
