namespace Eumis.Web.Api.Companies.DataObjects
{
    public class LocalActionGroupMunicipalityDO
    {
        public LocalActionGroupMunicipalityDO(int companyId, byte[] version)
        {
            this.CompanyId = companyId;
            this.Version = version;
        }

        public int CompanyId { get; set; }

        public byte[] Version { get; set; }

        public int? MunicipalityId { get; set; }
    }
}
