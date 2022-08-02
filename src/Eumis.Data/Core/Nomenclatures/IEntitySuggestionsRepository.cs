using System.Collections.Generic;

namespace Eumis.Data.Core.Nomenclatures
{
    public interface IEntitySuggestionsRepository<TEntity> : IRepository
    {
        IList<string> GetSuggestions(string term, int offset = 0, int? limit = null);
    }
}
