using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportRevalidation
{
    public interface IContractReportRevalidationService
    {
        Domain.Contracts.ContractReportRevalidation CreateContractReportRevalidation(
            int userId,
            ContractReportRevalidationType type,
            DateTime date,
            int? programmeId,
            int? programmePriorityId,
            int? procedureId,
            int? contractId,
            int? contractReportPaymentId);

        IList<string> CanEnterContractReportRevalidation(int contractReportRevalidationId);

        IList<string> CanMakeDraftContractReportRevalidation(int contractReportRevalidationId);
    }
}
