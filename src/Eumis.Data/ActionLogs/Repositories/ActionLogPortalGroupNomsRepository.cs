using System.Collections.Generic;
using System.Linq;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Log.ActionLogger;

namespace Eumis.Data.ActionLogs.Repositories
{
    internal class ActionLogPortalGroupNomsRepository : IActionLogPortalGroupNomsRepository
    {
        public ActionLogPortalGroupNomsRepository()
        {
        }

        public EntityNomVO GetNom(int id)
        {
            EntityNomVO entityNomVo = null;

            var groupInfo = ActionLogGroupUtils.GetPortalActionLogGroupInfoById(id);

            if (groupInfo != null)
            {
                entityNomVo = new EntityNomVO()
                {
                    NomValueId = groupInfo.Id,
                    Name = groupInfo.DisplayName,
                };
            }

            return entityNomVo;
        }

        public IList<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null)
        {
            string predicateTerm = !string.IsNullOrWhiteSpace(term) ? term.ToLower() : term;

            var predicate =
                PredicateBuilder.True<ActionLogGroupInfo>()
                .AndStringContains(e => e.DisplayName.ToLower(), predicateTerm);

            return ActionLogGroupUtils.GetPortalActionLogInfoItems().AsQueryable()
                .Where(predicate)
                .OrderBy(p => p.DisplayName)
                .WithOffsetAndLimit(offset, limit)
                .Select(e =>
                    new EntityNomVO()
                    {
                        NomValueId = e.Id,
                        Name = e.DisplayName,
                    })
                    .OrderBy(e => e.Name)
                    .ToList();
        }
    }
}
