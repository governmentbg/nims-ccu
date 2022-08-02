using Autofac.Features.AttributeFilters;
using Eumis.Data.ContractReportCertAuthorityCorrections.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertAuthorityCorrection
{
    internal class ContractReportCertAuthorityCorrectionClaimsContext : ClaimsContext, IContractReportCertAuthorityCorrectionClaimsContext
    {
        private int contractReportCertAuthorityCorrectionId;

        private IClaimsCache claimsCache;
        private IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository;

        public ContractReportCertAuthorityCorrectionClaimsContext(
            int contractReportCertAuthorityCorrectionId,
            [KeyFilter(ClaimsCaches.ContractReportCertAuthorityCorrection)]IClaimsCache claimsCache,
            IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository)
            : base(claimsCache)
        {
            this.contractReportCertAuthorityCorrectionId = contractReportCertAuthorityCorrectionId;
            this.claimsCache = claimsCache;
            this.contractReportCertAuthorityCorrectionsRepository = contractReportCertAuthorityCorrectionsRepository;
        }

        public int ContractReportCertAuthorityCorrectionId
        {
            get
            {
                return this.contractReportCertAuthorityCorrectionId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCertAuthorityCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractReportCertAuthorityCorrectionsRepository.GetProgrammeId(this.contractReportCertAuthorityCorrectionId));
            }
        }
    }
}
