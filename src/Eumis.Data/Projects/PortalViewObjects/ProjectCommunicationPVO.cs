using Eumis.Data.Core;
using Eumis.Data.Registrations.PortalViewObjects;

namespace Eumis.Data.Projects.PortalViewObjects
{
    public class ProjectCommunicationPVO
    {
        public PagePVO<ProjectCommunicationQuestionPVO> Communications { get; set; }

        public int Count { get; set; }

        public string ProjectRegNumber { get; set; }
    }
}
