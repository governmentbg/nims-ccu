using Eumis.Common.Json;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionStandingProjectDO
    {
        public EvalSessionStandingProjectDO()
        {
        }

        public int EvalSessionId { get; set; }

        public int ProjectId { get; set; }

        public string ProcedureName { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProjectName { get; set; }

        public string CompanyName { get; set; }

        public DateTime? ProjectRegDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectRegistrationStatus? ProjectRegistrationStatus { get; set; }

        public bool? IsPassedASD { get; set; }

        public decimal? PointsASD { get; set; }

        public bool? IsPassedTFO { get; set; }

        public decimal? PointsTFO { get; set; }

        public bool? IsPassedComplex { get; set; }

        public decimal? PointsComplex { get; set; }

        public bool? IsPassedPreliminary { get; set; }

        public decimal? PointsPreliminary { get; set; }

        public bool? IsApprovedInPreliminaryStanding { get; set; }

        public bool IsProjectDeleted { get; set; }

        public int? OrderNum { get; set; }

        public int? ManualOrderNum { get; set; }

        public bool CanMoveUp { get; set; }

        public bool CanMoveDown { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionProjectStandingStatus? Status { get; set; }

        public EvalSessionProjectStandingStatus? StatusName { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionProjectStandingStatus? ManualStatus { get; set; }

        public EvalSessionProjectStandingStatus? ManualStatusName { get; set; }

        public decimal? GrandAmount { get; set; }

        public bool IsStandingDeleted { get; set; }
    }
}
