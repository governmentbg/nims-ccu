using Eumis.Common.Json;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectMassManagingAuthorityCommunicationVO
    {
        public int ProjectMassManagingAuthorityCommunicationId { get; set; }

        public string ProcedureCode { get; set; }

        public string ProgrammeName { get; set; }

        public int OrderNum { get; set; }

        public DateTime? EndingDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectManagingAuthorityCommunicationSubject? Subject { get; set; }

        public DateTime ModifyDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectMassManagingAuthorityCommunicationStatus Status { get; set; }
    }
}
