using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/nomenclatures/contracts")]
    public class ContractNomsController : EntityNomsController<Contract, EntityNomVO>
    {
        private IContractNomsRepository contractNomsRepository;

        public ContractNomsController(IContractNomsRepository contractNomsRepository)
            : base(contractNomsRepository)
        {
            this.contractNomsRepository = contractNomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int procedureId, string term = null, int offset = 0, int? limit = null)
        {
            return this.contractNomsRepository.GetContracts(procedureId, term, offset, limit);
        }
    }
}
