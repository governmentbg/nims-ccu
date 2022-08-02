using Eumis.Domain.Core;
using Eumis.Domain.Projects;

namespace Eumis.Web.Api.ProjectManagingAuthorityCommunications.DataObjects
{
    public class ProjectMassManagingAuthorityCommunicationDocumentDO
    {
        public ProjectMassManagingAuthorityCommunicationDocumentDO()
        {
        }

        public ProjectMassManagingAuthorityCommunicationDocumentDO(int communicationId, byte[] version)
            : this()
        {
            this.CommunicationId = communicationId;
            this.Version = version;
        }

        public ProjectMassManagingAuthorityCommunicationDocumentDO(ProjectMassManagingAuthorityCommunicationDocument document, byte[] version)
            : this(document.ProjectMassManagingAuthorityCommunicationId, version)
        {
            this.ProjectMassManagingAuthorityCommunicationDocumentId = document.ProjectMassManagingAuthorityCommunicationDocumentId;
            this.Name = document.Name;
            this.Description = document.Description;
            this.File = document.FileKey.HasValue ? new FileDO
            {
                Key = document.FileKey.Value,
                Name = document.FileName,
            }
            : null;
        }

        public int ProjectMassManagingAuthorityCommunicationDocumentId { get; set; }

        public int CommunicationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
