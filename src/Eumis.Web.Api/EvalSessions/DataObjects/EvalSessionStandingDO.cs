using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionStandingDO
    {
        protected EvalSessionStandingDO()
        {
        }

        public EvalSessionStandingDO(int evalSessionStandingId)
        {
            this.EvalSessionStandingId = evalSessionStandingId;
        }

        public EvalSessionStandingDO(
            EvalSessionStanding evalSessionStanding,
            IList<EvalSessionStandingProjectDO> projects,
            byte[] version,
            bool canRearrange)
        {
            this.EvalSessionId = evalSessionStanding.EvalSessionId;
            this.EvalSessionStandingId = evalSessionStanding.EvalSessionStandingId;
            this.Code = evalSessionStanding.Code;
            this.IsPreliminary = evalSessionStanding.IsPreliminary;
            this.PreliminaryBudgetPercentage = evalSessionStanding.PreliminaryBudgetPercentage;
            this.Status = evalSessionStanding.Status;
            this.StatusNote = evalSessionStanding.StatusNote;
            this.StatusDate = evalSessionStanding.StatusDate;
            this.Projects = projects;

            this.CanRearrange = canRearrange;
            this.Version = version;
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionStandingId { get; set; }

        public string Code { get; set; }

        public bool IsPreliminary { get; set; }

        public bool CanRearrange { get; set; }

        public int? PreliminaryBudgetPercentage { get; set; }

        public EvalSessionStandingStatus Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime StatusDate { get; set; }

        public IList<EvalSessionStandingProjectDO> Projects { get; set; }

        public byte[] Version { get; set; }
    }
}
