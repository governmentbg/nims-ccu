using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractFinanceReport : ContractDocumentXml
    {
        public IList<ContractReportDocument> ProcedureContractReportFinancialDocuments { get; set; }
    }
}