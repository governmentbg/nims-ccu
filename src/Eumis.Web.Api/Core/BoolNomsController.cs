using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Web.Api.Core
{
    [RoutePrefix("api/nomenclatures/boolean")]
    public class BoolNomsController : ApiController
    {
        private readonly List<BoolNomVO> values = new List<BoolNomVO>
        {
            new BoolNomVO(true, WebApiTexts.BoolNomVO_True),
            new BoolNomVO(false, WebApiTexts.BoolNomVO_False),
        };

        public BoolNomsController()
        {
        }

        [Route("{id:bool}")]
        public BoolNomVO GetNom(bool id)
        {
            return this.values.Where(v => v.NomValueId == id).Single();
        }

        [Route("")]
        public IList<BoolNomVO> GetNoms(string term = null)
        {
            return this.values.Where(v => string.IsNullOrEmpty(term) || v.Name.Contains(term)).ToList();
        }
    }
}
