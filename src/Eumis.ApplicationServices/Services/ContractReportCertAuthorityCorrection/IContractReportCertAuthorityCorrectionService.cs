using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportCertAuthorityCorrection
{
    public interface IContractReportCertAuthorityCorrectionService
    {
        Domain.Contracts.ContractReportCertAuthorityCorrection CreateContractReportCertAuthorityCorrection(
            int userId,
            ContractReportCertAuthorityCorrectionType type,
            Sign sign,
            DateTime date,
            int? programmeId,
            int? programmePriorityId,
            int? procedureId,
            int? contractId,
            int? contractReportPaymentId);

        IList<string> CanEnterContractReportCertAuthorityCorrection(int contractReportCertAuthorityCorrectionId);

        IList<string> CanMakeDraftContractReportCertAuthorityCorrection(int contractReportCertAuthorityCorrectionId);
    }
}
