using Autofac.Features.AttributeFilters;
using Eumis.Data.CompensationDocuments.Repositories;
using Eumis.Data.ContractReportCorrections.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCorrection
{
    internal class ContractReportCorrectionClaimsContext : ClaimsContext, IContractReportCorrectionClaimsContext
    {
        private int contractReportCorrectionId;

        private IClaimsCache claimsCache;
        private IContractReportCorrectionsRepository contractReportCorrectionsRepository;

        public ContractReportCorrectionClaimsContext(
            int contractReportCorrectionId,
            [KeyFilter(ClaimsCaches.ContractReportCorrection)]IClaimsCache claimsCache,
            IContractReportCorrectionsRepository contractReportCorrectionsRepository)
            : base(claimsCache)
        {
            this.contractReportCorrectionId = contractReportCorrectionId;
            this.claimsCache = claimsCache;
            this.contractReportCorrectionsRepository = contractReportCorrectionsRepository;
        }

        public int ContractReportCorrectionId
        {
            get
            {
                return this.contractReportCorrectionId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractReportCorrectionsRepository.GetProgrammeId(this.contractReportCorrectionId));
            }
        }

        public int? ContractId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCorrectionId,
                    new ClaimKey("ContractId"),
                    () => this.contractReportCorrectionsRepository.GetContractId(this.contractReportCorrectionId));
            }
        }
    }
}
