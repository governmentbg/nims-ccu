using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/contractBudgetLevel3s")]
    public class ContractBudgetLevel3AmountNomsController : EntityNomsController<ContractBudgetLevel3Amount, EntityNomVO>
    {
        private IContractBudgetLevel3NomsRepository contractBudgetLevel3NomsRepository;

        public ContractBudgetLevel3AmountNomsController(IContractBudgetLevel3NomsRepository contractBudgetLevel3NomsRepository)
            : base(contractBudgetLevel3NomsRepository)
        {
            this.contractBudgetLevel3NomsRepository = contractBudgetLevel3NomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetContractContractBudgetLevel3s(int contractContractId, string term = null, int offset = 0, int? limit = null)
        {
            return this.contractBudgetLevel3NomsRepository.GetContractContractBudgetLevel3s(contractContractId, term, offset, limit);
        }
    }
}
