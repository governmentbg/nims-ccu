using System.Collections.Generic;
using Eumis.Data.Prognoses.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Prognoses.Repositories
{
    public interface IPrognosesRepository : IAggregateRepository<Prognosis>
    {
        IList<PrognosisVO> GetPrognoses(
            int[] programmeIds,
            PrognosisLevel level,
            Year? year,
            Month? month);

        IList<string> CanCreateProgrammePrognosis(
            int programmeId,
            Year year,
            Month month);

        IList<string> CanCreateProgrammePriorityPrognosis(
            int programmePriorityId,
            Year year,
            Month month);

        IList<string> CanCreateProcedurePrognosis(
            int procedureId,
            Year year,
            Month month);

        IList<PrognosisYearlyReportVO> GetYearlyPrognosisReport(int programmeId, Year[] years);

        IList<PrognosisMonthlyReportVO> GetMonthlyPrognosisReport(int programmeId, Year year, Month[] months);

        IList<PrognosisProgrammePriorityReportVO> GetProgrammePriorityPrognosisReport(int programmePriorityId);

        IList<PrognosisProgrammeReportVO> GetProgrammePrognosisReport(int programmeId);

        IList<PrognosisSummaryReportVO> GetPrognosisSummaryReport();

        int GetProgrammePrognosisProgrammeId(int prognosisId);

        int GetProgrammePriorityPrognosisProgrammeId(int prognosisId);

        int GetProcedurePrognosisProgrammeId(int prognosisId);
    }
}
