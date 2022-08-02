using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.UserTypes.Repositories
{
    public interface IUserNomsRepository : IEntityNomsRepository<User, EntityNomVO>
    {
        EntityNomVO GetExtendedUserNom(int userId);
    }
}
