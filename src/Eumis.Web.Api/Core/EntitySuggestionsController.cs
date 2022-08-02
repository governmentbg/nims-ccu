using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Web.Api.Core
{
    public class EntitySuggestionsController<TEntity> : ApiController
    {
        private IEntitySuggestionsRepository<TEntity> suggestionsRepository;

        public EntitySuggestionsController(IEntitySuggestionsRepository<TEntity> suggestionsRepository)
        {
            this.suggestionsRepository = suggestionsRepository;
        }

        [Route("")]
        public IList<string> GetSuggestions(string term = null, int offset = 0, int? limit = null)
        {
            return this.suggestionsRepository.GetSuggestions(term, offset, limit);
        }
    }
}
