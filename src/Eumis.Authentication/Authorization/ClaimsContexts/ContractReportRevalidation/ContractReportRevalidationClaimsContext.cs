using Autofac.Features.AttributeFilters;
using Eumis.Data.CompensationDocuments.Repositories;
using Eumis.Data.ContractReportRevalidations.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidation
{
    internal class ContractReportRevalidationClaimsContext : ClaimsContext, IContractReportRevalidationClaimsContext
    {
        private int contractReportRevalidationId;

        private IClaimsCache claimsCache;
        private IContractReportRevalidationsRepository contractReportRevalidationsRepository;

        public ContractReportRevalidationClaimsContext(
            int contractReportRevalidationId,
            [KeyFilter(ClaimsCaches.ContractReportRevalidation)]IClaimsCache claimsCache,
            IContractReportRevalidationsRepository contractReportRevalidationsRepository)
            : base(claimsCache)
        {
            this.contractReportRevalidationId = contractReportRevalidationId;
            this.claimsCache = claimsCache;
            this.contractReportRevalidationsRepository = contractReportRevalidationsRepository;
        }

        public int ContractReportRevalidationId
        {
            get
            {
                return this.contractReportRevalidationId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportRevalidationId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractReportRevalidationsRepository.GetProgrammeId(this.contractReportRevalidationId));
            }
        }
    }
}
