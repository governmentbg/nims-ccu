using Eumis.Public.Data.ProgrammeGroups.ViewObjects;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammeGroups;
using System.Collections.Generic;

namespace Eumis.Public.Data.ProgrammeGroups.Repositories
{
    public interface IProgrammeGroupsRepository
    {
        IEnumerable<ProgrammeGroup> GetAllProgrammeGroups();

        ProgrammeGroup GetProgrammeGroup(int programmeGroupId);

        IList<ProgrammeBudgetDetailedVO> GetProgrammeBudgetDetailed(bool getProgrammeGroups, int? programmeGroupId = null);

        IList<ProgrammeBudgetTotalsVO> GetProgrammeBudgetTotals(bool getProgrammeGroups, int? programmeGroupId = null);

        IList<FinanceSourceBudgetTotalsVO> GetFinanceSourceTotals(bool getProgrammeGroups, int? programmeGroupId = null);

        IList<ProgrammeFinanceSourceBudgetsVO> GetFinanceSourceTotalsByProgramme(bool getProgrammeGroups, int? programmeGroupId = null);
    }
}
