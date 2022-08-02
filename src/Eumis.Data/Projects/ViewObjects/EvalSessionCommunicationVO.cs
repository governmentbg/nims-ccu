using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;
using Newtonsoft.Json;

namespace Eumis.Data.Projects.ViewObjects
{
    public class EvalSessionCommunicationVO
    {
        public int ProjectCommunicationId { get; set; }

        public int ProjectId { get; set; }

        public int EvalSessionId { get; set; }

        public DateTime CreateDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectCommunicationStatus Status { get; set; }

        public string RegNumber { get; set; }

        public DateTime? QuestionDate { get; set; }

        public DateTime? QuestionEndingDate { get; set; }

        public DateTime? AnswerDate { get; set; }

        public DateTime? QuestionReadDate { get; set; }

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

        public string ProjectRegNumber { get; set; }
    }
}
