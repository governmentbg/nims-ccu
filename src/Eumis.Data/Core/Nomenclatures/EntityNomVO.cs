using Newtonsoft.Json;

namespace Eumis.Data.Core.Nomenclatures
{
    [JsonConverter(typeof(EntityNomVOJsonConverter))]
    public class EntityNomVO
    {
        public int NomValueId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }
    }
}
