using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/companySizeTypes")]
    public class CompanySizeTypeNomsController : EntityNomsController<CompanySizeType, CompanySizeTypeGidNomVO>
    {
        private ICompanySizeTypeNomsRepository nomsRepository;

        public CompanySizeTypeNomsController(ICompanySizeTypeNomsRepository nomsRepository)
            : base(nomsRepository)
        {
            this.nomsRepository = nomsRepository;
        }

        [Route("{valueAlias}")]
        public EntityGidNomVO GetByAlias(string valueAlias)
        {
            return this.nomsRepository.GetByAlias(valueAlias);
        }
    }
}
