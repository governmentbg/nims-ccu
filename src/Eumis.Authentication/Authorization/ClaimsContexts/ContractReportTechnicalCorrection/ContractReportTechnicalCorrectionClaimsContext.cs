using Autofac.Features.AttributeFilters;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReportTechnicalCorrections.Repositories;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportTechnicalCorrection
{
    internal class ContractReportTechnicalCorrectionClaimsContext : ClaimsContext, IContractReportTechnicalCorrectionClaimsContext
    {
        private int contractReportTechnicalCorrectionId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportTechnicalCorrectionsRepository contractReportTechnicalCorrectionsRepository;

        public ContractReportTechnicalCorrectionClaimsContext(
            int contractReportTechnicalCorrectionId,
            [KeyFilter(ClaimsCaches.ContractReportTechnicalCorrection)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportTechnicalCorrectionsRepository contractReportTechnicalCorrectionsRepository)
            : base(claimsCache)
        {
            this.contractReportTechnicalCorrectionId = contractReportTechnicalCorrectionId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportTechnicalCorrectionsRepository = contractReportTechnicalCorrectionsRepository;
        }

        public int ContractReportTechnicalCorrectionId
        {
            get
            {
                return this.contractReportTechnicalCorrectionId;
            }
        }

        public int ContractReportId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportTechnicalCorrectionId,
                    new ClaimKey("ContractReportId"),
                    () => this.contractReportTechnicalCorrectionsRepository.GetContractReportId(this.contractReportTechnicalCorrectionId));
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportTechnicalCorrectionId,
                    new ClaimKey("ContractId"),
                    () => this.contractReportsRepository.GetContractId(this.ContractReportId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportTechnicalCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
