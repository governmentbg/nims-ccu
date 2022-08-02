using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectMonitorstatRequestsVO
    {
        public int ProjectMonitorstatRequestId { get; set; }

        public string RequestName { get; set; }

        public string CompanyUin { get; set; }

        public ProjectMonitorstatRequestStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectMonitorstatRequestStatus StatusDescr
        {
            get
            {
                return this.Status;
            }
        }

        public Guid FileKey { get; set; }

        public string Declaration { get; set; }

        public string User { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public IList<ProjectMonitorstatResponseVO> Responses { get; set; }
    }
}
