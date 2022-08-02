using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureInterventionCategoryVO
    {
        public int? InterventionCategoryId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public Dimension Dimension { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ActiveStatus ActiveStatus { get; set; }

        public byte[] Version { get; set; }
    }
}
