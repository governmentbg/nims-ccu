using System;
using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionStandpointVO
    {
        public int EvalSessionId { get; set; }

        public int EvalSessionStandpointId { get; set; }

        public Guid XmlGid { get; set; }

        public string User { get; set; }

        public int ProjectId { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProjectName { get; set; }

        public string Note { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionStandpointStatus? StatusName { get; set; }

        public EvalSessionStandpointStatus Status { get; set; }
    }
}
