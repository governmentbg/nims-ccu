using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractTechnicalReport : ContractDocumentXml
    {
        public IList<ContractReportDocument> ProcedureContractReportTechnicalDocuments { get; set; }
    }
}