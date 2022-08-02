using Eumis.Common.Json;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class InternalЕnvironmentSpotCheckVO
    {
        public int SpotCheckId { get; set; }

        public string ProgrammeName { get; set; }

        public string ContractRegNum { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckType Type { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public IList<SpotCheckAscertainmentContentVO> Ascertainments { get; set; }

        public IList<SpotCheckRecommendationContentVO> Recommendations { get; set; }

        public IList<SpotCheckRecommendationExecutionStatusVO> RecommendationExecutionStatuses { get; set; }
    }
}
