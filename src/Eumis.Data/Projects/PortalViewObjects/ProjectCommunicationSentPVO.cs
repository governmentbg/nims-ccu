using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using System;

namespace Eumis.Data.Projects.PortalViewObjects
{
    public class ProjectCommunicationSentPVO
    {
        public Guid RegisteredGid { get; set; }

        public string CommunicationRegNumber { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProcedureCode { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNameAlt { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public EnumPVO<ProjectManagingAuthorityCommunicationSubject> Subject { get; set; }
    }
}
