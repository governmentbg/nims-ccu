using Autofac.Features.AttributeFilters;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractProcurement
{
    internal class ContractProcurementClaimsContext : ClaimsContext, IContractProcurementClaimsContext
    {
        private int procurementId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;

        public ContractProcurementClaimsContext(
            int procurementId,
            [KeyFilter(ClaimsCaches.ContractProcurement)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractProcurementsRepository contractProcurementsRepository)
            : base(claimsCache)
        {
            this.procurementId = procurementId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
        }

        public int ContractProcurementId
        {
            get
            {
                return this.procurementId;
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.procurementId,
                    new ClaimKey("ContractId"),
                    () => this.contractProcurementsRepository.GetContractId(this.procurementId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.procurementId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
