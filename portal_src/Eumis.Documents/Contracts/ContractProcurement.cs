using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractProcurements : ContractDocumentXml
    {
        public IList<ContractReportDocument> ContractProcurementDocuments { get; set; }

        public IList<string> ContractProcurementPlansWithCheckSheets { get; set; }

    }

    public class ContractProcurementsOffer
    {
        public Guid OfferGid { get; set; }
        public DateTime OfferSubmitDate { get; set; }

        public Guid DifferentiatedPositionGid { get; set; }
        public Guid ProcurementPlanGid { get; set; }
    }
}