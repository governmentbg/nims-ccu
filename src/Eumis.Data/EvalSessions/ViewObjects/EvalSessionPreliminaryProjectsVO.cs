using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionPreliminaryProjectsVO : EvalSessionResultProjectsVO
    {
        public decimal? GrantAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionEvaluationResult PreliminaryResult { get; set; }

        public decimal? Points { get; set; }

        public int? OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionProjectStandingStatus Status { get; set; }

        public string Note { get; set; }
    }
}
