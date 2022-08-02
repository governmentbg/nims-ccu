using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/nomenclatures/procedureYears")]
    public class ProcedureYearNomsController : ApiController
    {
        private IList<EntityNomVO> years;

        public ProcedureYearNomsController()
        {
            var listYears = Enumerable.Range(2021, 10).ToList();
            this.years = listYears.Select(t => new EntityNomVO
            {
                NomValueId = t,
                Name = $"{t} г.",
                NameAlt = t.ToString(),
            }).ToList();
        }

        [Route("{year:int}")]
        public EntityNomVO GetNom(int year)
        {
            return this.years
                .Where(t => t.NomValueId == year)
                .FirstOrDefault();
        }

        [Route("")]
        public IList<EntityNomVO> GetNoms(string term = null)
        {
            if (!string.IsNullOrEmpty(term))
            {
                this.years.Where(t => t.Name.Contains(term));
            }

            return this.years;
        }
    }
}
