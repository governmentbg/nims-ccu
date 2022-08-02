using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public class ContractReportMicrosType1Item
    {
        public int ContractReportMicrosType1ItemId { get; set; }

        public int ContractReportMicroId { get; set; }

        public int? DistrictId { get; set; }

        public int? MunicipalityId { get; set; }

        public int? TotalCount { get; set; }

        public int? ChildrensCount { get; set; }

        public int? SeniorsCount { get; set; }

        public int? FemalesCount { get; set; }

        public int? EmigrantsCount { get; set; }

        public int? ForeignCitizensCount { get; set; }

        public int? MinoritiesCount { get; set; }

        public int? GypsiesCount { get; set; }

        public int? DisabledPersonsCount { get; set; }

        public int? HomelessCount { get; set; }
    }

    public class ContractReportMicrosType1ItemMap : EntityTypeConfiguration<ContractReportMicrosType1Item>
    {
        public ContractReportMicrosType1ItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportMicrosType1ItemId);

            // Properties
            this.Property(t => t.ContractReportMicrosType1ItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.ContractReportMicroId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportMicrosType1Items");
            this.Property(t => t.ContractReportMicrosType1ItemId).HasColumnName("ContractReportMicrosType1ItemId");
            this.Property(t => t.ContractReportMicroId).HasColumnName("ContractReportMicroId");

            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");

            this.Property(t => t.TotalCount).HasColumnName("TotalCount");
            this.Property(t => t.ChildrensCount).HasColumnName("ChildrensCount");
            this.Property(t => t.SeniorsCount).HasColumnName("SeniorsCount");
            this.Property(t => t.FemalesCount).HasColumnName("FemalesCount");
            this.Property(t => t.EmigrantsCount).HasColumnName("EmigrantsCount");
            this.Property(t => t.ForeignCitizensCount).HasColumnName("ForeignCitizensCount");
            this.Property(t => t.MinoritiesCount).HasColumnName("MinoritiesCount");
            this.Property(t => t.GypsiesCount).HasColumnName("GypsiesCount");
            this.Property(t => t.DisabledPersonsCount).HasColumnName("DisabledPersonsCount");
            this.Property(t => t.HomelessCount).HasColumnName("HomelessCount");
        }
    }
}
