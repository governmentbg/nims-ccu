using System;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Domain.NonAggregates;

namespace Eumis.Web.Api.CompensationDocuments.DataObjects
{
    public class NewCompensationDocumentDO
    {
        public CompensationDocumentType? Type { get; set; }

        public CompensationSign? CompensationSign { get; set; }

        public DateTime? CompensationDocDate { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ContractId { get; set; }

        public int? ContractReportPaymentId { get; set; }
    }
}
