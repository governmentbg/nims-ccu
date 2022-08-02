using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectMonitorstatMassRequestDO
    {
        public ProjectMonitorstatMassRequestDO(
            int projectId,
            byte[] version,
            int projectVersionXmlId,
            IList<Rio.Company> projectVersionCompanies)
        {
            this.ProjectId = projectId;
            this.Version = version;
            this.ProjectVersionXmlId = projectVersionXmlId;
            this.Companies = projectVersionCompanies
                .Select(c => new ProjectMonitorstatRequestCompanyDO(c))
                .ToList();
        }

        public int ProjectId { get; set; }

        public byte[] Version { get; set; }

        public int ProjectVersionXmlId { get; set; }

        public IList<ProjectMonitorstatRequestCompanyDO> Companies { get; set; }
    }
}
