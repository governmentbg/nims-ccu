using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public class SpotCheckPlace
    {
        public int CountryId { get; set; }

        public int? DistrictId { get; set; }

        public int? MunicipalityId { get; set; }

        public int? SettlementId { get; set; }

        public string Address { get; set; }

        public void UpdateAttributes(
            int countryId,
            int? districtId,
            int? municipalityId,
            int? settlementId,
            string address)
        {
            this.CountryId = countryId;
            this.DistrictId = districtId;
            this.MunicipalityId = municipalityId;
            this.SettlementId = settlementId;
            this.Address = address;
        }
    }

    public class SpotCheckPlaceMap : ComplexTypeConfiguration<SpotCheckPlace>
    {
        public SpotCheckPlaceMap()
        {
            this.Property(t => t.CountryId)
                .IsRequired();

            this.Property(t => t.CountryId).HasColumnName("PlaceCountryId");
            this.Property(t => t.DistrictId).HasColumnName("PlaceDistrictId");
            this.Property(t => t.MunicipalityId).HasColumnName("PlaceMunicipalityId");
            this.Property(t => t.SettlementId).HasColumnName("PlaceSettlementId");
            this.Property(t => t.Address).HasColumnName("PlaceAddress");
        }
    }
}
