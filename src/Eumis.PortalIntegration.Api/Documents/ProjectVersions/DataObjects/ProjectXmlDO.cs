using Eumis.PortalIntegration.Api.Core;
using System;

namespace Eumis.PortalIntegration.Api.Documents.ProjectVersions.DataObjects
{
    public class ProjectXmlDO : XmlDO
    {
        public DateTime CreateDate { get; set; }

        public ProjectRegDataDO RegData { get; set; }
    }
}
