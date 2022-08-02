using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/contractVersionStatuses")]
    public class ContractVersionStatusNomsController : EnumNomsController<ContractVersionStatus>
    {
        public ContractVersionStatusNomsController(IEnumNomsRepository<ContractVersionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
