using System.Collections.Generic;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.SpotChecks;

namespace Eumis.ApplicationServices.Services.SpotCheck
{
    public interface ISpotCheckService
    {
        bool CanCreatePlannedCheck(int userId, int spotCheckPlanId);

        bool CanCreateCheck(int userId, int programmeId, SpotCheckLevel level, int? contractId);

        IList<string> CanEnterCheck(int spotCheckId);

        void AddItems(Domain.SpotChecks.SpotCheck check, int userId, int[] itemIds);
    }
}
