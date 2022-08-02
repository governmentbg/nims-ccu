using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.UserTypes;
using System.Collections.Generic;

namespace Eumis.Data.UserTypes.Repositories
{
    public interface IUserTypeNomsRepository : IEntityNomsRepository<UserType, EntityNomVO>
    {
        IList<EntityNomVO> GetUserTypeNoms(int userOrganizationId, string term = null, int offset = 0, int? limit = null);
    }
}
