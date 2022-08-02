using Eumis.Domain.Projects;
using Eumis.PortalIntegration.Api.Core;
using System;

namespace Eumis.PortalIntegration.Api.Documents.ProjectCommunications.DataObjects
{
    public class ProjectCommunicationXmlDO : XmlDO
    {
        public ProjectRegDataDO RegData { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public DateTime? MessageEndingDate { get; set; }

        public ProjectManagingAuthorityCommunicationSubject? Subject { get; set; }
    }
}
