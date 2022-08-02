using System;
using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectRegistrationsVO
    {
        public int ProjectId { get; set; }

        public int ProcedureId { get; set; }

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

        public string RegNumber { get; set; }

        public string Name
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.NameBg, this.NameEn);
            }
        }

        [JsonIgnore]
        public string NameBg { get; set; }

        [JsonIgnore]
        public string NameEn { get; set; }

        public string KidCode { get; set; }

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

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType CompanyUinType { get; set; }

        public string CompanyUin { get; set; }

        public string CompanySizeType
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.CompanySizeTypeBg, this.CompanySizeTypeEn);
            }
        }

        [JsonIgnore]
        public string CompanySizeTypeBg { get; set; }

        [JsonIgnore]
        public string CompanySizeTypeEn { get; set; }

        public string CompanyKidCode { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectRegistrationStatus RegistrationStatus { get; set; }

        public string ProjectType
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.ProjectTypeBg, this.ProjectTypeEn);
            }
        }

        [JsonIgnore]
        public string ProjectTypeBg { get; set; }

        [JsonIgnore]
        public string ProjectTypeEn { get; set; }

        public DateTime? RegDate { get; set; }

        public byte[] Version { get; set; }
    }
}
