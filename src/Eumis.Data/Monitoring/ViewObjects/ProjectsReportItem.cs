using System;
using Eumis.Common.Json;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class ProjectsReportItem
    {
        public string Programme { get; set; }

        public string Procedure { get; set; }

        public string RegNumber { get; set; }

        public string Name { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime RecieveDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectRecieveType RecieveType { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyName { get; set; }

        public string CompanyType { get; set; }

        public string CompanyLegalType { get; set; }

        public string CompanyKidCode { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyCorrespondenceAddress { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanySizeType { get; set; }

        public int? ProjectDuration { get; set; }

        public string ProjectPlace { get; set; }

        public string ProjectKidCode { get; set; }

        public decimal? InitialTotalBfpAmount { get; set; }

        public decimal? InitialCoFinancingAmount { get; set; }

        public decimal? ActualTotalBfpAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectsReportItemEvalResult? IsAdminAdmissPassed { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectsReportItemEvalResult? IsTechFinancePassed { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectsReportItemEvalResult? IsComplexPassed { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectsReportItemStandingStatus? StandingStatus { get; set; }
    }
}
