using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Projects
{
    public partial class ProjectMonitorstatRequest
    {
        public void AssertSendIsAllowed()
        {
            if (this.Status != ProjectMonitorstatRequestStatus.Draft && this.Status != ProjectMonitorstatRequestStatus.Failed)
            {
                throw new DomainValidationException("Cannot modify project monitorstat request that is not in Draft or Failed status");
            }
        }

        #region ProjectMonitorstatResponse

        public void AddMonitorstatResponse(string fileName, Guid fileKey)
        {
            this.MonitorstatResponses.Add(new ProjectMonitorstatResponse(fileName, fileKey));
            this.Status = ProjectMonitorstatRequestStatus.Finished;
        }

        #endregion ProjectMonitorstatResponse
    }
}
