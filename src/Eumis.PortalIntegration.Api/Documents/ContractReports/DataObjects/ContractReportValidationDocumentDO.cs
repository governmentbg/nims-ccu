using System;

namespace Eumis.PortalIntegration.Api.Documents.ContractReports.DataObjects
{
    public class ContractReportValidationDocumentDO
    {
        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsRequired { get; set; }

        public bool IsActive { get; set; }
    }
}
