using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class NewEvalSessionStandingDO : EvalSessionStandingDO
    {
        public NewEvalSessionStandingDO(int evalSessionId, NewEvalSessionStandingType type, IList<EvalSessionStandingProjectDO> projects, byte[] version)
        {
            this.EvalSessionId = evalSessionId;
            this.IsPreliminary = type == NewEvalSessionStandingType.Preliminary;
            this.Type = type;
            this.Status = EvalSessionStandingStatus.Applied;
            this.StatusDate = DateTime.Now;
            this.Projects = projects;

            this.Version = version;
        }

        public NewEvalSessionStandingType Type { get; set; }
    }
}
