using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractProcurementDocument : ContractDocumentXml
    {
        public IList<ContractReportDocument> ProcedureProcurementDocuments { get; set; }

        public IList<string> ContractProcurementPlansWithCheckSheets { get; set; }

    }
}
