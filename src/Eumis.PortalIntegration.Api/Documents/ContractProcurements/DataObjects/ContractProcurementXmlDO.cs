using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.PortalIntegration.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.PortalIntegration.Api.Documents.ContractProcurements.DataObjects
{
    public class ContractProcurementXmlDO : XmlDO
    {
        public ContractProcurementXmlDO(string xml, byte[] version, IList<ProcedureContractReportDocument> procedureProcurementDocuments)
        {
            this.Xml = xml;
            this.Version = version;

            this.ContractProcurementDocuments = procedureProcurementDocuments
            .Select(crd => new ContractProcurementValidationDocumentDO()
            {
                Gid = crd.Gid,
                Name = crd.Name,
                Extension = crd.Extension,
                IsRequired = crd.IsRequired,
                IsActive = crd.IsActive,
            })
            .ToList();
        }

        public IList<ContractProcurementValidationDocumentDO> ContractProcurementDocuments { get; set; }
    }
}
