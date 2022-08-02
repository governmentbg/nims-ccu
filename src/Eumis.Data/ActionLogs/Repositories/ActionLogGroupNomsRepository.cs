using System.Collections.Generic;
using System.Linq;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Data.ActionLogs.Repositories
{
    internal class ActionLogGroupNomsRepository : IActionLogGroupNomsRepository
    {
        public ActionLogGroupNomsRepository()
        {
        }

        public EntityNomVO GetNom(int id)
        {
            EntityNomVO entityNomVo = null;

            var groupInfo = ActionLogGroupUtils.GetActionLogGroupInfoById(id);

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

        public IList<EntityNomVO> GetNoms(bool procedureActionsOnly = false, string term = null, int offset = 0, int? limit = null)
        {
            string predicateTerm = !string.IsNullOrWhiteSpace(term) ? term.ToLower() : term;

            var predicate =
                PredicateBuilder.True<ActionLogGroupInfo>()
                .AndStringContains(e => e.DisplayName.ToLower(), predicateTerm);

            if (procedureActionsOnly)
            {
                var procedureGroupKey = string.Format("{0}.", ActionLogGroupUtils.GetClassDescriptionKey(typeof(ActionLogGroups.Procedures)));

                predicate = predicate.And(e => e.Key.StartsWith(procedureGroupKey));
            }

            return ActionLogGroupUtils.GetActionLogInfoItems().AsQueryable()
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
