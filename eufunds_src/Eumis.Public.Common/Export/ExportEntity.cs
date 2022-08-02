namespace Eumis.Public.Common.Export
{
    public class ExportEntity
    {
        public ExportEntity(string id)
        {
            this.Id = id;
        }

        public string Id { get; }

        public string EntityUin { get; set; }

        public string EntityName { get; set; }

        public string EntityAddress { get; set; }

        public string EntityZipCode { get; set; }

        public string EntityCity { get; set; }

        public string EntityMunicipality { get; set; }

        public string EntityDistrict { get; set; }
    }
}
