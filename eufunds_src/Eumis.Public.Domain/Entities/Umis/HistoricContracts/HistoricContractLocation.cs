using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.HistoricContracts
{
    public class HistoricContractLocation
    {
        public int HistoricContractLocationId { get; set; }

        public int HistoricContractId { get; set; }

        public string CountryCode { get; set; }

        public string ProtectedZoneCode { get; set; }

        public string Nuts1Code { get; set; }

        public string Nuts2Code { get; set; }

        public string DistrictCode { get; set; }

        public string MunicipalityCode { get; set; }

        public string SettlementCode { get; set; }

        public string FullPath { get; set; }

        public string FullPathName { get; set; }

        public virtual HistoricContract HistoricContract { get; set; }
    }

    public class HistoricContractLocationMap : EntityTypeConfiguration<HistoricContractLocation>
    {
        public HistoricContractLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoricContractLocationId);

            // Properties
            this.Property(t => t.HistoricContractLocationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HistoricContractId)
                .IsRequired();

            this.Property(t => t.CountryCode)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ProtectedZoneCode)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Nuts1Code)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Nuts2Code)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.DistrictCode)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.MunicipalityCode)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.SettlementCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.FullPath)
                .IsRequired();

            this.Property(t => t.FullPathName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("HistoricContractLocations");
            this.Property(t => t.HistoricContractLocationId).HasColumnName("HistoricContractLocationId");
            this.Property(t => t.HistoricContractId).HasColumnName("HistoricContractId");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            this.Property(t => t.ProtectedZoneCode).HasColumnName("ProtectedZoneCode");
            this.Property(t => t.Nuts1Code).HasColumnName("Nuts1Code");
            this.Property(t => t.Nuts2Code).HasColumnName("Nuts2Code");
            this.Property(t => t.DistrictCode).HasColumnName("DistrictCode");
            this.Property(t => t.MunicipalityCode).HasColumnName("MunicipalityCode");
            this.Property(t => t.SettlementCode).HasColumnName("SettlementCode");
            this.Property(t => t.FullPath).HasColumnName("FullPath");
            this.Property(t => t.FullPathName).HasColumnName("FullPathName");

            // Relationships
            this.HasRequired(t => t.HistoricContract)
                .WithMany(t => t.HistoricContractLocations)
                .HasForeignKey(d => d.HistoricContractId)
                .WillCascadeOnDelete();
        }
    }
}
