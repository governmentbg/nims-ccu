using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/companyLegalTypes")]
    public class CompanyLegalTypeNomsController : EntityNomsController<CompanyLegalType, CompanyLegalTypeGidNomVO>
    {
        private ICompanyLegalTypeNomsRepository companyLegalTypeNomsRepository;

        public CompanyLegalTypeNomsController(ICompanyLegalTypeNomsRepository companyLegalTypeNomsRepository)
            : base(companyLegalTypeNomsRepository)
        {
            this.companyLegalTypeNomsRepository = companyLegalTypeNomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityGidNomVO> GetCompanyLegalTypesByCompanyType(int companyTypeId, string term = null, int offset = 0, int? limit = null)
        {
            return this.companyLegalTypeNomsRepository.GetCompanyLegalTypeNoms(companyTypeId, term, offset, limit);
        }
    }
}
