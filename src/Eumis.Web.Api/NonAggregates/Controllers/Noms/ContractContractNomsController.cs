using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/contractContracts")]
    public class ContractContractNomsController : ApiController
    {
        private IContractContractNomsRepository contractContractNomsRepository;

        public ContractContractNomsController(IContractContractNomsRepository contractContractNomsRepository)
        {
            this.contractContractNomsRepository = contractContractNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.contractContractNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int contractId, string term = null, int offset = 0, int? limit = null)
        {
            return this.contractContractNomsRepository.GetContractContracts(contractId, term, offset, limit);
        }
    }
}
