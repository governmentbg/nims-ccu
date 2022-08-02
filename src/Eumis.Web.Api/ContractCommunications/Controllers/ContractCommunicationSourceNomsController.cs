using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractCommunications.Controllers
{
    [RoutePrefix("api/nomenclatures/contractCommunicationSources")]
    public class ContractCommunicationSourceNomsController : EnumNomsController<ContractCommunicationSource>
    {
        public ContractCommunicationSourceNomsController(IEnumNomsRepository<ContractCommunicationSource> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
