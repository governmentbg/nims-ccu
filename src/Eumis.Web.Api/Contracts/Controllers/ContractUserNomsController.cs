using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Measures;
using Eumis.Domain.Users;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Users.Controllers
{
    [RoutePrefix("api/nomenclatures/contractUsers")]
    public class ContractUserNomsController : EntityNomsController<User, EntityNomVO>
    {
        private IContractUserNomsRepository contractUserNomsRepository;

        public ContractUserNomsController(IContractUserNomsRepository contractUserNomsRepository)
            : base(contractUserNomsRepository)
        {
            this.contractUserNomsRepository = contractUserNomsRepository;
        }

        [Route("")]
        public IList<EntityNomVO> GetNoms(int contractId, string term = null, int offset = 0, int? limit = null)
        {
            return this.contractUserNomsRepository.GetContractUserNoms(contractId, term, offset, limit);
        }
    }
}
