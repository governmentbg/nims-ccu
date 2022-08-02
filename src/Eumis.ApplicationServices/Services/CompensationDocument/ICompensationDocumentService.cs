using System;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Domain.NonAggregates;

namespace Eumis.ApplicationServices.Services.CompensationDocument
{
    public interface ICompensationDocumentService
    {
        Domain.MonitoringFinancialControl.CompensationDocuments.CompensationDocument CreateCompensationDocument(
            int userId,
            CompensationDocumentType type,
            CompensationSign compensationSign,
            DateTime compensationDocDate,
            int contractId,
            int programmePriorityId,
            int? contractReportPaymentId);
    }
}
