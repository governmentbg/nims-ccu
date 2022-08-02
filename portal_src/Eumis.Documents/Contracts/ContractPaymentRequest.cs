using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractPaymentRequest : ContractDocumentXml
    {
        public bool? contractReportHasAdvanceVerificationPayment { get; set; }

        public IList<ContractReportDocument> ProcedureContractReportPaymentDocuments { get; set; }
    }
}
