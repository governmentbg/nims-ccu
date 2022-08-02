using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/contractTypes")]
    public class ContractTypeNomsController : EnumNomsController<ContractType>
    {
        public ContractTypeNomsController(IEnumNomsRepository<ContractType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
