using Newtonsoft.Json;
using System;

namespace Eumis.Data.Core.Nomenclatures
{
    [JsonConverter(typeof(EntityGidNomVOJsonConverter))]
    public class EntityGidNomVO
    {
        public int NomValueId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }
    }
}
