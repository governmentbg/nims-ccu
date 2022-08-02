using Eumis.Data.Companies.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Companies;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Companies.Controllers
{
    [RoutePrefix("api/nomenclatures/companies")]
    public class CompanyNomsController : ApiController
    {
        private ICompanyNomsRepository companyNomsRepository;

        public CompanyNomsController(ICompanyNomsRepository companyNomsRepository)
        {
            this.companyNomsRepository = companyNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.companyNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetLocalActionGroups(string term = null, int offset = 0, int? limit = null)
        {
            return this.companyNomsRepository.GetLocalActionGroups(term, offset, limit);
        }
    }
}
