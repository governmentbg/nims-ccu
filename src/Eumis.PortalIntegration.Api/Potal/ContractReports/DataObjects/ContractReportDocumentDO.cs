using System;

namespace Eumis.PortalIntegration.Api.Potal.ContractReports.DataObjects
{
    public class ContractReportDocumentDO
    {
        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsRequired { get; set; }

        public bool IsActive { get; set; }
    }
}
