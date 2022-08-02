using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.ContractReports.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.PortalIntegration.Api.Potal.ContractProcurements.DataObjects
{
    public class ContractProcurementDO : XmlDO
    {
        public ContractProcurementDO(ContractProcurementXml procurement, IList<ProcedureContractReportDocument> procedureProcurementDocuments)
        {
            this.Xml = procurement.Xml;
            this.Version = procurement.Version;
            this.ModifyDate = procurement.ModifyDate;

            this.ProcedureProcurementDocuments = procedureProcurementDocuments.Select(crd => new ContractReportDocumentDO()
            {
                Gid = crd.Gid,
                Name = crd.Name,
                Extension = crd.Extension,
                IsRequired = crd.IsRequired,
                IsActive = crd.IsActive,
            }).ToList();
        }

        public IList<ContractReportDocumentDO> ProcedureProcurementDocuments { get; set; }
    }
}
