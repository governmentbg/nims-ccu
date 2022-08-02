using Eumis.Common.Json;
using Eumis.Domain.CertAuthorityChecks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.CertAuthorityChecks.ViewObjects
{
    public class CertAuthorityCheckVO
    {
        public int CertAuthorityCheckId { get; set; }

        public int? CheckNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertAuthorityCheckStatus StatusDescr { get; set; }

        public CertAuthorityCheckStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertAuthorityCheckType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertAuthorityCheckSubjectType? SubjectType { get; set; }

        public string SubjectName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public IList<string> ProgrammeShortNames { get; set; }

        public IList<string> ItemCodes { get; set; }

        public IList<string> ProjectCodes { get; set; }

        public IList<CertAuthorityCheckAscertainmentContentVO> Ascertainments { get; set; }

        public IList<CertAuthorityCheckAscertainmentExecutionStatusVO> RecommendationExecutionStatuses { get; set; }
    }
}
