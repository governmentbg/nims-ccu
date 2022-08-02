using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractCommunications.Controllers
{
    [RoutePrefix("api/nomenclatures/contractCommunicationStatuses")]
    public class ContractCommunicationStatusNomsController : EnumNomsController<ContractCommunicationStatus>
    {
        public ContractCommunicationStatusNomsController(IEnumNomsRepository<ContractCommunicationStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
