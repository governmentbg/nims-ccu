using System.Collections.Generic;
using Eumis.Data.SpotChecks.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.SpotChecks;

namespace Eumis.Data.SpotChecks.Repositories
{
    public interface ISpotCheckPlansRepository : IAggregateRepository<SpotCheckPlan>
    {
        IList<SpotCheckPlanVO> GetSpotCheckPlans(
            int[] programmeIds,
            int userId,
            Year? year = null,
            Month? month = null);

        SpotCheckPlanInfoVO GetInfo(int spotCheckPlanId);

        SpotCheckPlanBasicDataVO GetBasicData(int spotCheckPlanId);

        IList<SpotCheckDocVO> GetSpotCheckPlanDocs(int spotCheckPlanId);

        IList<SpotCheckTargetVO> GetSpotCheckPlanTargets(int spotCheckPlanId);

        IList<SpotCheckProgrammePriorityItemVO> GetProgrammePriorityItems(int spotCheckPlanId);

        int[] GetProgrammePriorityIds(int spotCheckPlanId);

        IList<SpotCheckProcedureItemVO> GetProcedureItems(int spotCheckPlanId);

        int[] GetProcedureIds(int spotCheckPlanId);

        IList<SpotCheckContractItemVO> GetContractItems(int spotCheckPlanId);

        int[] GetContractIds(int spotCheckPlanId);

        IList<SpotCheckContractContractItemVO> GetContractContractItems(int spotCheckPlanId);

        int[] GetContractContractIds(int spotCheckPlanId);

        IList<string> CanDeletePlan(int spotCheckPlanId);

        bool IsCheckPlanUnique(int programmeId, Year year, Month month);

        bool HasAssociatedCheck(int spotCheckPlanId);

        int GetProgrammeId(int spotCheckPlanId);

        int? GetContractId(int spotCheckPlanId);
    }
}
