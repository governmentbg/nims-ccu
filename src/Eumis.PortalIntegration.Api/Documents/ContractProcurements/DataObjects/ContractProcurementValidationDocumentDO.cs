using System;

namespace Eumis.PortalIntegration.Api.Documents.ContractProcurements.DataObjects
{
    public class ContractProcurementValidationDocumentDO
    {
        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsRequired { get; set; }

        public bool IsActive { get; set; }
    }
}
