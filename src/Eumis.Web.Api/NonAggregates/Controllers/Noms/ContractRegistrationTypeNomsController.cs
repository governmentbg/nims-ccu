using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/contractRegistrationTypes")]
    public class ContractRegistrationTypeNomsController : EnumNomsController<ContractRegistrationType>
    {
        public ContractRegistrationTypeNomsController(IEnumNomsRepository<ContractRegistrationType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
