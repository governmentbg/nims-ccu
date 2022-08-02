using Eumis.Common.Json;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectMassManagingAuthorityCommunicationInfoVO
    {
        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectMassManagingAuthorityCommunicationStatus StatusName
        {
            get
            {
                return this.Status;
            }
        }

        public ProjectMassManagingAuthorityCommunicationStatus Status { get; set; }

        public string ProcedureCode { get; set; }

        public byte[] Version { get; set; }
    }
}
