using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportCorrection
{
    public interface IContractReportCorrectionService
    {
        Domain.Contracts.ContractReportCorrection CreateContractReportCorrection(
            int userId,
            ContractReportCorrectionType type,
            Sign sign,
            DateTime date,
            int? programmeId,
            int? programmePriorityId,
            int? procedureId,
            int? contractId,
            int? contractReportPaymentId);

        IList<string> CanEnterContractReportCorrection(int contractReportCorrectionId);

        IList<string> CanMakeDraftContractReportCorrection(int contractReportCorrectionId);

        string GetContractReportCorrectionElementNumber(Domain.Contracts.ContractReportCorrection contractReportCorrection);
    }
}
