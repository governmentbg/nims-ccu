using System;
using System.Collections.Generic;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;

namespace Eumis.ApplicationServices.Services.Irregularity
{
    public interface IIrregularityService
    {
        Domain.Irregularities.Irregularity CreateIrregularity(
            int userId,
            int irregularitySignalId,
            DateTime irregularityDateFrom,
            DateTime? irregularityDateTo,
            Year reportYear,
            Quarter reportQuarter,
            bool shouldReportToOlaf,
            IrregularityReasonNotReportingToOlaf? reasonNotReportingToOlaf,
            IrregularitySanctionProcedureType sanctionProcedureType,
            IrregularitySanctionProcedureKind? sanctionProcedureKind,
            DateTime? sanctionProcedureStartDate,
            DateTime? sanctionProcedureExpectedEndDate,
            DateTime? sanctionProcedureEndDate,
            IrregularitySanctionProcedureStatus? sanctionProcedureStatus,
            int? sanctionCategoryId,
            int? sanctionTypeId,
            string fines);

        IList<string> CanUpdatePartial(int programmeId, int signalId, string signalNumber);

        void DeleteIrregularity(int irregularityId, byte[] version);

        IList<string> CanDeleteIrregularity(int irregularityId);

        void AddFinancialCorrections(Domain.Irregularities.Irregularity irregularity, int userId, int[] financialCorrectionIds);
    }
}
