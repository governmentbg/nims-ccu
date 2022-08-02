using System;
using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionProjectsVO
    {
        public int EvalSessionId { get; set; }

        public int ProjectId { get; set; }

        public string ProcedureName { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProjectName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.ProjectNameBg, this.ProjectNameEn);
            }
        }

        [JsonIgnore]
        public string ProjectNameBg { get; set; }

        [JsonIgnore]
        public string ProjectNameEn { get; set; }

        public string ProjectKidCode { get; set; }

        public string Company
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.CompanyBg, this.CompanyEn);
            }
        }

        [JsonIgnore]
        public string CompanyBg { get; set; }

        [JsonIgnore]
        public string CompanyEn { get; set; }

        public string CompanyKidCode { get; set; }

        public DateTime? ProjectRegDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectRegistrationStatus? ProjectRegistrationStatus { get; set; }

        public ProjectRegistrationStatus? ProjectRegistrationStatusName { get; set; }

        public bool? IsPassedASD { get; set; }

        public decimal? PointsASD { get; set; }

        public bool? IsPassedTFO { get; set; }

        public decimal? PointsTFO { get; set; }

        public bool? IsPassedComplex { get; set; }

        public decimal? PointsComplex { get; set; }

        public bool? IsPassedPreliminary { get; set; }

        public decimal? PointsPreliminary { get; set; }

        public string EvalSessionNum { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public int? EvalSessionProjectStandingId { get; set; }

        public int? OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionProjectStandingStatus? StandingStatus { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionProjectWorkStatus? WorkStatus { get; set; }
    }
}
