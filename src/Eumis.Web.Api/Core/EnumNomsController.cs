using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Web.Api.Core
{
    public class EnumNomsController<TEnum> : ApiController
        where TEnum : struct, IConvertible
    {
        private IEnumNomsRepository<TEnum> nomsRepository;

        public EnumNomsController(IEnumNomsRepository<TEnum> nomsRepository)
        {
            this.nomsRepository = nomsRepository;
        }

        [Route("{id}")]
        public EnumNomVO<TEnum> GetNom(TEnum id)
        {
            return this.nomsRepository.GetNom(id);
        }

        [Route("")]
        public IList<EnumNomVO<TEnum>> GetNoms(string term = null)
        {
            return this.nomsRepository.GetNoms(term);
        }
    }
}
