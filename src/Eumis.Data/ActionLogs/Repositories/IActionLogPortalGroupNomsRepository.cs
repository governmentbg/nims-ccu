using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Data.ActionLogs.Repositories
{
    public interface IActionLogPortalGroupNomsRepository
    {
        EntityNomVO GetNom(int id);

        IList<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null);
    }
}
