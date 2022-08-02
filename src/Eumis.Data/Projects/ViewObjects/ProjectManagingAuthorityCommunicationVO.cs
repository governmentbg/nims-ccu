using Eumis.Common.Json;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectManagingAuthorityCommunicationVO
    {
        public int ProjectCommunicationId { get; set; }

        public int ProjectId { get; set; }

        public int OrderNum { get; set; }

        public DateTime? QuestionSendDate { get; set; }

        public DateTime? QuestionReadDate { get; set; }

        public DateTime? AnswerDate { get; set; }

        public string ProjectRegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectManagingAuthorityCommunicationStatus StatusDescr { get; set; }

        public ProjectManagingAuthorityCommunicationStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectManagingAuthorityCommunicationSource Source { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectManagingAuthorityCommunicationSubject Subject { get; set; }
    }
}
