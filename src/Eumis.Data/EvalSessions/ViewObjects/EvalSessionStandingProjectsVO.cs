using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionStandingProjectsVO : EvalSessionResultProjectsVO
    {
        public decimal? GrantAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        public bool? IsPassedPreliminary { get; set; }

        public decimal? PointsPreliminary { get; set; }

        public bool? IsPassedASD { get; set; }

        public decimal? PointsASD { get; set; }

        public bool? IsPassedTFO { get; set; }

        public decimal? PointsTFO { get; set; }

        public bool? IsPassedComplex { get; set; }

        public decimal? PointsComplex { get; set; }

        public int? OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionProjectStandingStatus Status { get; set; }

        public decimal? CorrectedGrantAmount { get; set; }

        public decimal? CorrectedSelfAmount { get; set; }

        public string Note { get; set; }
    }
}
