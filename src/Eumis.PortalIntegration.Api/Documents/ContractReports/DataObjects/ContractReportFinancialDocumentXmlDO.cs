using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.PortalIntegration.Api.Core;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.PortalIntegration.Api.Documents.ContractReports.DataObjects
{
    public class ContractReportFinancialDocumentXmlDO : ContractReportDocumentXmlDO
    {
        public ContractReportFinancialDocumentXmlDO(
            string xml,
            byte[] version,
            IList<string> canEnterErrors,
            IList<ProcedureContractReportDocument> documents)
            : base(xml, version, canEnterErrors)
        {
            this.ProcedureContractReportFinancialDocuments = documents
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

        public IList<ContractReportValidationDocumentDO> ProcedureContractReportFinancialDocuments { get; set; }
    }
}
