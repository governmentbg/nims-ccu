using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Data.ActionLogs.Repositories
{
    public interface IActionLogGroupNomsRepository
    {
        EntityNomVO GetNom(int id);

        IList<EntityNomVO> GetNoms(bool procedureActionsOnly = false, string term = null, int offset = 0, int? limit = null);
    }
}
