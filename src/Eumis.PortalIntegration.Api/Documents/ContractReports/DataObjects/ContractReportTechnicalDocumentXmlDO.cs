using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.PortalIntegration.Api.Core;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.PortalIntegration.Api.Documents.ContractReports.DataObjects
{
    public class ContractReportTechnicalDocumentXmlDO : ContractReportDocumentXmlDO
    {
        public ContractReportTechnicalDocumentXmlDO(
            string xml,
            byte[] version,
            IList<string> canEnterErrors,
            IList<ProcedureContractReportDocument> documents)
            : base(xml, version, canEnterErrors)
        {
            this.ProcedureContractReportTechnicalDocuments = documents
                .Select(crd => new ContractReportValidationDocumentDO()
                {
                    Gid = crd.Gid,
                    Name = crd.Name,
                    Extension = crd.Extension,
                    IsRequired = crd.IsRequired,
                    IsActive = crd.IsActive,
                })
                .ToList();
        }

        public IList<ContractReportValidationDocumentDO> ProcedureContractReportTechnicalDocuments { get; set; }
    }
}
