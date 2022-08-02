using System;
using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Data.Projects.ViewObjects
{
    public class CommunicationRegVO
    {
        public ProjectCommunicationStatus Status { get; set; }

        public string ProgrammeName { get; set; }

        public string ProcedureName { get; set; }

        public string ProjectName { get; set; }

        public string ProjectRegNumber { get; set; }

        public string RegNumber { get; set; }

        public DateTime? RegDate { get; set; }

        public string AnswerXmlHash { get; set; }

        public string CompanyName { get; set; }

        public string Uin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType UinType { get; set; }
    }
}
