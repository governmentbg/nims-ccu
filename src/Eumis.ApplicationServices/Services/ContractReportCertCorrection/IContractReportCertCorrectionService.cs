using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportCertCorrection
{
    public interface IContractReportCertCorrectionService
    {
        Domain.Contracts.ContractReportCertCorrection CreateContractReportCertCorrection(
            int userId,
            ContractReportCertCorrectionType type,
            Sign sign,
            DateTime date,
            int? programmeId,
            int? programmePriorityId,
            int? procedureId,
            int? contractId,
            int? contractReportPaymentId);

        IList<string> CanEnterContractReportCertCorrection(int contractReportCertCorrectionId);

        IList<string> CanMakeDraftContractReportCertCorrection(int contractReportCertCorrectionId);
    }
}
