using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/contractVersionTypes")]
    public class ContractVersionTypeNomsController : EnumNomsController<ContractVersionType>
    {
        public ContractVersionTypeNomsController(IEnumNomsRepository<ContractVersionType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
