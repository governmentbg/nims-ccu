using Eumis.Common.Json;
using Eumis.Domain.Audits;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eumis.Data.Audits.ViewObjects
{
    public class AuditVO
    {
        public int AuditId { get; set; }

        public string ProgrammeName { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AuditInstitution AuditInstitution { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AuditType AuditType { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AuditKind AuditKind { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AuditLevel Level { get; set; }

        public IList<string> ItemCodes { get; set; }

        public IList<string> ProjectCodes { get; set; }

        public IList<AuditAscertainmentContentVO> Ascertainments { get; set; }

        public IList<AuditAscertainmentFulfilledStatusVO> RecommendationsFulfilledStatuses { get; set; }
    }
}
