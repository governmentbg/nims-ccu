using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractProcurementPlans")]
    public class ContractProcurementPlanNomsController : EntityNomsController<ContractProcurementPlan, EntityNomVO>
    {
        private IContractProcurementPlanNomsRepository contractProcurementPlanNomsRepository;

        public ContractProcurementPlanNomsController(IContractProcurementPlanNomsRepository contractProcurementPlanNomsRepository)
            : base(contractProcurementPlanNomsRepository)
        {
            this.contractProcurementPlanNomsRepository = contractProcurementPlanNomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetContractProcurementPlans(int contractId, string term = null, int offset = 0, int? limit = null)
        {
            return this.contractProcurementPlanNomsRepository.GetContractProcurementPlans(contractId, term, offset, limit);
        }
    }
}
