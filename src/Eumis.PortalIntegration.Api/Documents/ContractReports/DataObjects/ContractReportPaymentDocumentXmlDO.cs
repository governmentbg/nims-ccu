using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.PortalIntegration.Api.Core;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.PortalIntegration.Api.Documents.ContractReports.DataObjects
{
    public class ContractReportPaymentDocumentXmlDO : ContractReportDocumentXmlDO
    {
        public ContractReportPaymentDocumentXmlDO(
            string xml,
            byte[] version,
            bool? contractReportHasAdvanceVerificationPayment,
            IList<string> canEnterErrors,
            IList<ProcedureContractReportDocument> documents)
            : base(xml, version, canEnterErrors)
        {
            this.ContractReportHasAdvanceVerificationPayment = contractReportHasAdvanceVerificationPayment;

            this.ProcedureContractReportPaymentDocuments = documents
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

        public bool? ContractReportHasAdvanceVerificationPayment { get; set; }

        public IList<ContractReportValidationDocumentDO> ProcedureContractReportPaymentDocuments { get; set; }
    }
}
