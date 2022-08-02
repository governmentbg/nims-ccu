using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionDistributionProjectsVO
    {
        public int EvalSessionId { get; set; }

        public int ProjectId { get; set; }

        public string ProcedureName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.ProcedureNameBg, this.ProcedureNameEn);
            }
        }

        [JsonIgnore]
        public string ProcedureNameBg { get; set; }

        [JsonIgnore]
        public string ProcedureNameEn { get; set; }

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

        public string CompanyName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.CompanyNameBg, this.CompanyNameEn);
            }
        }

        [JsonIgnore]
        public string CompanyNameBg { get; set; }

        [JsonIgnore]
        public string CompanyNameEn { get; set; }

        public DateTime? ProjectRegDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectRegistrationStatus? ProjectRegistrationStatus { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public bool? IsPassedASD { get; set; }

        public bool? IsPassedTFO { get; set; }

        public decimal? TotalEvaluationTFO { get; set; }
    }
}
