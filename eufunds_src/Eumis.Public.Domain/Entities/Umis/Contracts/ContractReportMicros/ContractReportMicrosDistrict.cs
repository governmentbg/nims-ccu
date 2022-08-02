using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public partial class ContractReportMicrosDistrict
    {
        public int ContractReportMicrosDistrictId { get; set; }

        public int DistrictId { get; set; }

        public string Name { get; set; }
    }

    public class ContractReportMicrosDistrictMap : EntityTypeConfiguration<ContractReportMicrosDistrict>
    {
        public ContractReportMicrosDistrictMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportMicrosDistrictId);

            // Properties
            this.Property(t => t.ContractReportMicrosDistrictId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.DistrictId)
                .IsRequired();
            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportMicrosDistricts");
            this.Property(t => t.ContractReportMicrosDistrictId).HasColumnName("ContractReportMicrosDistrictId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
