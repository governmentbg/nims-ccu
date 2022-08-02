using Eumis.Domain.Core;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectMassManagingAuthorityCommunicationDocumentVO
    {
        public int DocumentId { get; set; }

        public int ProjectMassManagingAuthorityCommunicationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
