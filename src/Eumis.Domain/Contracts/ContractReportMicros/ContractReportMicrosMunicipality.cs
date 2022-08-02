using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public partial class ContractReportMicrosMunicipality
    {
        public int ContractReportMicrosMunicipalityId { get; set; }

        public int ContractReportMicrosDistrictId { get; set; }

        public int MunicipalityId { get; set; }

        public string Name { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportMicrosMunicipalityMap : EntityTypeConfiguration<ContractReportMicrosMunicipality>
    {
        public ContractReportMicrosMunicipalityMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportMicrosMunicipalityId);

            // Properties
            this.Property(t => t.ContractReportMicrosMunicipalityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.ContractReportMicrosDistrictId)
                .IsRequired();
            this.Property(t => t.MunicipalityId)
                .IsRequired();
            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportMicrosMunicipalities");
            this.Property(t => t.ContractReportMicrosMunicipalityId).HasColumnName("ContractReportMicrosMunicipalityId");
            this.Property(t => t.ContractReportMicrosDistrictId).HasColumnName("ContractReportMicrosDistrictId");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
