using System.Collections.Generic;
using Eumis.Data.SpotChecks.ViewObjects;
using Eumis.Domain.SpotChecks;

namespace Eumis.Data.SpotChecks.Repositories
{
    public interface ISpotChecksRepository : IAggregateRepository<SpotCheck>
    {
        IList<SpotCheckVO> GetSpotCheks(
            int[] programmeIds,
            int userId,
            int? programmeId,
            SpotCheckStatus? status = null,
            SpotCheckType? type = null);

        int GetProgrammeId(int spotCheckId);

        int? GetContractId(int spotCheckId);

        SpotCheckInfoVO GetInfo(int spotCheckId);

        SpotCheckBasicDataVO GetBasicData(int spotCheckId);

        IList<SpotCheckAscertainmentVO> GetSpotCheckAscertainments(int spotCheckId);

        IList<SpotCheckDocVO> GetSpotCheckDocs(int spotCheckId);

        IList<SpotCheckTargetVO> GetSpotCheckTargets(int spotCheckId);

        IList<SpotCheckRecommendationVO> GetSpotCheckRecommendations(int spotCheckId);

        IList<SpotCheckAscertainmentItemVO> GetNotIncludedAscertainments(int spotCheckId, int recommendationId);

        IList<SpotCheckAscertainmentItemVO> GetAscertainments(int spotCheckId, int recommendationId);

        IList<SpotCheckRecommendationItemVO> GetRecommendations(int spotCheckId, int ascertainmentId);

        IList<SpotCheckProgrammePriorityItemVO> GetProgrammePriorityItems(int spotCheckId);

        int[] GetProgrammePriorityIds(int spotCheckId);

        IList<SpotCheckProcedureItemVO> GetProcedureItems(int spotCheckId);

        int[] GetProcedureIds(int spotCheckId);

        IList<SpotCheckContractItemVO> GetContractItems(int spotCheckId);

        int[] GetContractIds(int spotCheckId);

        IList<SpotCheckContractContractItemVO> GetContractContractItems(int spotCheckId);

        int[] GetContractContractIds(int spotCheckId);

        bool IsSpotCheckNumUnique(int checkNum, int programmeId, int? spotCheckId);

        bool HasDocuments(int spotCheckId);
    }
}
