using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Projects.DataObjects;
using Newtonsoft.Json;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionProjectStandingDO
    {
        public EvalSessionProjectStandingDO()
        {
        }

        public EvalSessionProjectStandingDO(int evalSessionId, int projectId, bool isPreliminary, IList<EvalSessionEvaluationVO> projectEvaluations, byte[] version)
        {
            this.EvalSessionId = evalSessionId;
            this.ProjectId = projectId;
            this.IsPreliminary = isPreliminary;
            this.Type = EvalSessionProjectStandingType.Manual;
            this.CreateDate = DateTime.Now;
            this.ProjectEvaluations = projectEvaluations;

            this.Version = version;
        }

        public EvalSessionProjectStandingDO(EvalSessionProjectStanding evalSessionProjectStanding, IList<EvalSessionEvaluationVO> projectEvaluations, byte[] version)
        {
            this.EvalSessionId = evalSessionProjectStanding.EvalSessionId;
            this.EvalSessionProjectStandingId = evalSessionProjectStanding.EvalSessionProjectStandingId;
            this.ProjectId = evalSessionProjectStanding.ProjectId;
            this.IsPreliminary = evalSessionProjectStanding.IsPreliminary;
            this.OrderNum = evalSessionProjectStanding.OrderNum;
            this.Type = evalSessionProjectStanding.EvalSessionStandingId.HasValue ? EvalSessionProjectStandingType.Automatic : EvalSessionProjectStandingType.Manual;
            this.Status = evalSessionProjectStanding.Status;
            this.GrandAmount = evalSessionProjectStanding.GrandAmount;
            this.IsDeleted = evalSessionProjectStanding.IsDeleted;
            this.IsDeletedNote = evalSessionProjectStanding.IsDeletedNote;
            this.Notes = evalSessionProjectStanding.Notes;
            this.EvalSessionStandingId = evalSessionProjectStanding.EvalSessionStandingId;
            this.CreateDate = evalSessionProjectStanding.CreateDate;
            this.ProjectVersionXmlId = evalSessionProjectStanding.ProjectVersionXmlId;
            this.RejectionReasonId = evalSessionProjectStanding.RejectionReasonId;
            this.ProjectEvaluations = projectEvaluations;

            this.Version = version;
        }

        public int? EvalSessionId { get; set; }

        public int? EvalSessionProjectStandingId { get; set; }

        public int ProjectId { get; set; }

        public bool IsPreliminary { get; set; }

        public int? OrderNum { get; set; }

        public EvalSessionProjectStandingType Type { get; set; }

        public EvalSessionProjectStandingStatus? Status { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? GrandAmount { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public string Notes { get; set; }

        public int? EvalSessionStandingId { get; set; }

        public DateTime CreateDate { get; set; }

        public int ProjectVersionXmlId { get; set; }

        public int? RejectionReasonId { get; set; }

        public ProjectVersionDO ProjectVersion { get; set; }

        public IList<EvalSessionEvaluationVO> ProjectEvaluations { get; set; }

        public int? ProcedureBudgetComponentId { get; set; }

        public bool ProcedureHasBudgetComponent { get; set; }

        public byte[] Version { get; set; }
    }
}
