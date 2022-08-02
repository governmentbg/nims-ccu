using System.Collections.Generic;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Irregularities.Repositories
{
    public interface IIrregularitiesRepository : IAggregateRepository<Irregularity>
    {
        IList<IrregularityVO> GetIrregularities(int[] programmeIds, int userId);

        IrrByQuarterReportVO GetIrrByQuarterReport(Year year, Quarter quarter, int programmeId);

        IList<IrrRegisterItemVO> GetIrrRegister(
            int[] programmeIds,
            Year? reportYearFrom = null,
            Quarter? reportQuarterFrom = null,
            Year? reportYearTo = null,
            Quarter? reportQuarterTo = null);

        IList<IrrReportItemVO> GetIrrReport(
            int[] programmeIds,
            Year? reportYear = null,
            Quarter? reportQuarter = null,
            IrregularityCaseState? caseState = null);

        IList<IrrReportInvolvedPersonVO> GetReportInvolvedPersons(int[] versionIds);

        IList<IrrReportVersionDataVO> GetVersionsData(int[] irregularityIds);

        IList<IrregularityDocVO> GetDocuments(int irregularityId);

        IList<IrregularityFinancialCorrectionVO> GetFinancialCorrections(int irregularityId);

        IList<IrregularityFinancialCorrectionVO> GetNotIncludedFinancialCorrections(int irregularityId);

        IrregularityInfoVO GetInfo(int irregularityId);

        IrregularityDataVO GetData(int irregularityId);

        int GetProgrammeId(int irregularityId);

        IrregularityStatus GetIrregularityStatus(int irregularityId);

        bool HasFinancialCorrections(int irregularityId);

        bool HasNonRemovedIrregularityWithTheSameNumber(int programmeId, int irregularityId, string regNumber);

        bool HasRemovedRemovedIrregularityWithTheSameNumber(int programmeId, int irregularityId, string regNumber);

        IList<IrregularityVO> GetFinancialCorrectionIrregularities(int financialCorrectionId);

        IList<IrregularityVO> GetIrregularitiesForProjectDossier(int contractId);

        int? GetContractId(int irregularityId);
    }
}
