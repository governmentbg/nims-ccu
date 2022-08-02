using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionResultProjectsVO
    {
        public int EvalSessionResultProjectId { get; set; }

        public int ProjectId { get; set; }

        public string ProjectRegNumber { get; set; }

        public DateTime ProjectRegDate { get; set; }

        [JsonIgnore]
        public string ProjectNameBg { get; set; }

        [JsonIgnore]
        public string ProjectNameEn { get; set; }

        public string ProjectName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.ProjectNameBg, this.ProjectNameEn);
            }
        }

        [JsonIgnore]
        public string CompanyNameBg { get; set; }

        [JsonIgnore]
        public string CompanyNameEn { get; set; }

        public string CompanyName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.CompanyNameBg, this.CompanyNameEn);
            }
        }

        public string CompanyUin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType CompanyUinType { get; set; }
    }
}
