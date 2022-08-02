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
    public class EvalSessionAdminAdmissProjectsVO : EvalSessionResultProjectsVO
    {
        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionEvaluationResult AdminAdmissResult { get; set; }

        public string NonAdmissionReason { get; set; }
    }
}
