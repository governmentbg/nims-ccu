using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Projects.ViewObjects
{
    public class EvalSessionProjectMonitorstatRequestsVO
    {
        public int ProjectMonitorstatRequestId { get; set; }

        public int ProjectId { get; set; }

        public string CompanyUin { get; set; }

        public ProjectMonitorstatRequestStatus Status { get; set; }

        public string ProjectNameBg { get; set; }

        public string ProjectNameEn { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProjectName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.ProjectNameBg, this.ProjectNameEn);
            }
        }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectMonitorstatRequestStatus StatusDescr
        {
            get
            {
                return this.Status;
            }
        }

        public Guid? FileKey { get; set; }

        public string Declaration { get; set; }

        public string User { get; set; }

        public DateTime ModifyDate { get; set; }

        public IList<ProjectMonitorstatResponseVO> Responses { get; set; }
    }
}
