using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Data.Messages.Repositories
{
    public interface IMessageRecipientsNomsRepository
    {
        IList<EntityNomVO> GetUsers(int[] ids, string term, int offset, int? limit = null);
    }
}
