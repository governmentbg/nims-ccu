using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionDistributionDO
    {
        public EvalSessionDistributionDO()
        {
        }

        public EvalSessionDistributionDO(int evalSessionId, IList<EvalSessionDistributionUserDO> assessors, IList<EvalSessionDistributionProjectsVO> projects, byte[] version)
        {
            this.EvalSessionId = evalSessionId;
            this.Status = EvalSessionDistributionStatus.Draft;
            this.CreateDate = DateTime.Now;
            this.Assessors = assessors;
            this.Projects = projects;

            this.Version = version;
        }

        public EvalSessionDistributionDO(EvalSessionDistribution evalSessionDistribution, IList<EvalSessionDistributionUserDO> assessors, IList<EvalSessionDistributionProjectsVO> projects, byte[] version)
        {
            this.EvalSessionId = evalSessionDistribution.EvalSessionId;
            this.EvalSessionDistributionId = evalSessionDistribution.EvalSessionDistributionId;
            this.EvalTableType = evalSessionDistribution.EvalTableType;
            this.Code = evalSessionDistribution.Code;
            this.CreateDate = evalSessionDistribution.CreateDate;
            this.Status = evalSessionDistribution.Status;
            this.StatusNote = evalSessionDistribution.StatusNote;
            this.Status = evalSessionDistribution.Status;
            this.AssessorsPerProject = evalSessionDistribution.AssessorsPerProject;

            this.Assessors = evalSessionDistribution.EvalSessionDistributionUsers.Select(t =>
            {
                var assessor = assessors.Single(p => p.EvalSessionUserId == t.EvalSessionUserId);
                return new EvalSessionDistributionUserDO(t, assessor.Username, assessor.Fullname);
            }).OrderBy(p => p.Fullname).ToList();

            this.Projects = projects;

            this.Version = version;
        }

        public int? EvalSessionId { get; set; }

        public int? EvalSessionDistributionId { get; set; }

        public ProcedureEvalTableType? EvalTableType { get; set; }

        public string Code { get; set; }

        public DateTime CreateDate { get; set; }

        public EvalSessionDistributionStatus? Status { get; set; }

        public string StatusNote { get; set; }

        public int? AssessorsPerProject { get; set; }

        public IList<EvalSessionDistributionUserDO> Assessors { get; set; }

        public IList<EvalSessionDistributionProjectsVO> Projects { get; set; }

        public byte[] Version { get; set; }
    }
}
