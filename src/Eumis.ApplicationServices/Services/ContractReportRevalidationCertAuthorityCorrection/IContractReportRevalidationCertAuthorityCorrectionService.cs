using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportRevalidationCertAuthorityCorrection
{
    public interface IContractReportRevalidationCertAuthorityCorrectionService
    {
        Domain.Contracts.ContractReportRevalidationCertAuthorityCorrection CreateContractReportRevalidationCertAuthorityCorrection(
            int userId,
            ContractReportRevalidationCertAuthorityCorrectionType type,
            Sign sign,
            DateTime date,
            int? programmeId,
            int? programmePriorityId,
            int? procedureId,
            int? contractId,
            int? contractReportPaymentId);

        IList<string> CanEnterContractReportRevalidationCertAuthorityCorrection(int contractReportRevalidationCertAuthorityCorrectionId);

        IList<string> CanMakeDraftContractReportRevalidationCertAuthorityCorrection(int contractReportRevalidationCertAuthorityCorrectionId);
    }
}
