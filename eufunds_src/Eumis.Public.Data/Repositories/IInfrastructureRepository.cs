using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using System.Collections.Generic;

namespace Eumis.Public.Data.Repositories
{
    public interface IInfrastructureRepository
    {
        IEnumerable<Programme> GetAllOps();

        ICollection<Programme> GetEfmdrProgrammes();

        IEnumerable<MapNode> GetPriorityAxisForOp(int opId);

        IEnumerable<MapNode> GetInvestmentPrioritiesForAxis(int axisId);

        IEnumerable<MapNode> GetSpecificTargetForInvestment(int investmentId);
    }
}
