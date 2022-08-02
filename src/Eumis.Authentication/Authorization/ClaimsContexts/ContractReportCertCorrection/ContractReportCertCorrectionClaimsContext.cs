using Autofac.Features.AttributeFilters;
using Eumis.Data.CompensationDocuments.Repositories;
using Eumis.Data.ContractReportCertCorrections.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertCorrection
{
    internal class ContractReportCertCorrectionClaimsContext : ClaimsContext, IContractReportCertCorrectionClaimsContext
    {
        private int contractReportCertCorrectionId;

        private IClaimsCache claimsCache;
        private IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository;

        public ContractReportCertCorrectionClaimsContext(
            int contractReportCertCorrectionId,
            [KeyFilter(ClaimsCaches.ContractReportCertCorrection)]IClaimsCache claimsCache,
            IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository)
            : base(claimsCache)
        {
            this.contractReportCertCorrectionId = contractReportCertCorrectionId;
            this.claimsCache = claimsCache;
            this.contractReportCertCorrectionsRepository = contractReportCertCorrectionsRepository;
        }

        public int ContractReportCertCorrectionId
        {
            get
            {
                return this.contractReportCertCorrectionId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCertCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractReportCertCorrectionsRepository.GetProgrammeId(this.contractReportCertCorrectionId));
            }
        }
    }
}
