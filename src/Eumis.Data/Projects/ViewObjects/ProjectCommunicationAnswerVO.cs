using Eumis.Common.Json;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectCommunicationAnswerVO
    {
        public int ProjectCommunicationAnswerId { get; set; }

        public int ProjectCommunicationId { get; set; }

        public Guid XmlGid { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectCommunicationAnswerStatus StatusDescr { get; set; }

        public ProjectCommunicationAnswerStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectCommunicationAnswerSource Source { get; set; }

        public DateTime? AnswerDate { get; set; }
    }
}
