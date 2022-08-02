using System.Collections.Generic;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.SpotChecks;

namespace Eumis.ApplicationServices.Services.SpotCheck
{
    public interface ISpotCheckPlanService
    {
        IList<string> CanCreate(int userId, int programmeId, int? contractId, SpotCheckLevel level, Year year, Month month);

        void AddItems(SpotCheckPlan plan, int userId, int[] itemIds);
    }
}
