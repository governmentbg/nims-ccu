using Eumis.Common.Json;
using Eumis.Domain.Audits;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eumis.Data.Audits.ViewObjects
{
    public class InternalЕnvironmentAuditVO
    {
        public int AuditId { get; set; }

        public string ProgrammeName { get; set; }

        public string ContractRegNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AuditInstitution AuditInstitution { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AuditType AuditType { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AuditKind AuditKind { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AuditLevel Level { get; set; }

        public IList<AuditAscertainmentContentVO> Ascertainments { get; set; }

        public IList<AuditAscertainmentFulfilledStatusVO> RecommendationsFulfilledStatuses { get; set; }
    }
}
