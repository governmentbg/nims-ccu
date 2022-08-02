using Newtonsoft.Json;

namespace Eumis.Data.Core.Nomenclatures
{
    [JsonConverter(typeof(EntityCodeNomVOJsonConverter))]
    public class EntityCodeNomVO
    {
        public int NomValueId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }
    }
}
