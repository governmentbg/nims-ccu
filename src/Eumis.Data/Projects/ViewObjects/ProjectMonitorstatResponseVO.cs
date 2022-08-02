using System;
using Eumis.Domain.Projects;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectMonitorstatResponseVO
    {
        public ProjectMonitorstatResponseVO()
        {
        }

        public ProjectMonitorstatResponseVO(ProjectMonitorstatResponse response)
        {
            this.FileName = response.FileName;
            this.FileKey = response.FileKey;
            this.ModifyDate = response.ModifyDate;
        }

        public Guid FileKey { get; set; }

        public string FileName { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
