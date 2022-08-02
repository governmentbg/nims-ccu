using Autofac.Features.AttributeFilters;
using Eumis.Data.ContractReportRevalidationCertAuthorityCorrections.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidationCACorrection
{
    internal class ContractReportRevalidationCACorrectionClaimsContext : ClaimsContext, IContractReportRevalidationCACorrectionClaimsContext
    {
        private int contractReportRevalidationCertAuthorityCorrectionId;

        private IClaimsCache claimsCache;
        private IContractReportRevalidationCertAuthorityCorrectionsRepository contractReportRevalidationCertAuthorityCorrectionsRepository;

        public ContractReportRevalidationCACorrectionClaimsContext(
            int contractReportRevalidationCertAuthorityCorrectionId,
            [KeyFilter(ClaimsCaches.ContractReportRevalidationCertAuthorityCorrection)]IClaimsCache claimsCache,
            IContractReportRevalidationCertAuthorityCorrectionsRepository contractReportRevalidationCertAuthorityCorrectionsRepository)
            : base(claimsCache)
        {
            this.contractReportRevalidationCertAuthorityCorrectionId = contractReportRevalidationCertAuthorityCorrectionId;
            this.claimsCache = claimsCache;
            this.contractReportRevalidationCertAuthorityCorrectionsRepository = contractReportRevalidationCertAuthorityCorrectionsRepository;
        }

        public int ContractReportRevalidationCertAuthorityCorrectionId
        {
            get
            {
                return this.contractReportRevalidationCertAuthorityCorrectionId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportRevalidationCertAuthorityCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractReportRevalidationCertAuthorityCorrectionsRepository.GetProgrammeId(this.contractReportRevalidationCertAuthorityCorrectionId));
            }
        }
    }
}
