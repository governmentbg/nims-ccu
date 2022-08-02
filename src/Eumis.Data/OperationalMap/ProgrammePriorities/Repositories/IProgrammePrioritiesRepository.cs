using System.Collections.Generic;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Domain.OperationalMap.ProgrammePriorities;

namespace Eumis.Data.OperationalMap.ProgrammePriorities.Repositories
{
    public interface IProgrammePrioritiesRepository : IAggregateRepository<ProgrammePriority>
    {
        IList<ProgrammePriorityBudgetsWrapperVO> GetProgrammePriorityBudgets(int programmePriorityId);

        IList<ProgrammePriorityItemVO> GetProgrammePriorityItems(int programmeId, int[] exceptIds);

        string GetProgrammePriorityCode(int programmeId);

        IList<string> CanCreateProgrammePriority(int programmeId);

        IList<string> CanModifyProgrammePriority(
            int programmeId,
            int? programmePriorityId,
            string code,
            string name);

        IList<string> CanDeleteProgrammePriority(int programmePriorityId);

        int GetProgrammeId(int programmePriorityId);

        IList<MapNodeDirectionVO> GetProgrammePriorityDirections(int mapNodeId);
    }
}
