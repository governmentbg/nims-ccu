using Autofac.Features.AttributeFilters;
using Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidationCAFinancialCorrection
{
    internal class ContractReportRevalidationCAFinancialCorrectionClaimsContext : ClaimsContext, IContractReportRevalidationCAFinancialCorrectionClaimsContext
    {
        private int contractReportRevalidationCertAuthorityFinancialCorrectionId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository contractReportRevalidationCertAuthorityFinancialCorrectionsRepository;

        public ContractReportRevalidationCAFinancialCorrectionClaimsContext(
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            [KeyFilter(ClaimsCaches.ContractReportRevalidationCertAuthorityFinancialCorrection)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository contractReportRevalidationCertAuthorityFinancialCorrectionsRepository)
            : base(claimsCache)
        {
            this.contractReportRevalidationCertAuthorityFinancialCorrectionId = contractReportRevalidationCertAuthorityFinancialCorrectionId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository = contractReportRevalidationCertAuthorityFinancialCorrectionsRepository;
        }

        public int ContractReportRevalidationCertAuthorityFinancialCorrectionId
        {
            get
            {
                return this.contractReportRevalidationCertAuthorityFinancialCorrectionId;
            }
        }

        public int ContractReportId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportRevalidationCertAuthorityFinancialCorrectionId,
                    new ClaimKey("ContractReportId"),
                    () => this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.GetContractReportId(this.contractReportRevalidationCertAuthorityFinancialCorrectionId));
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportRevalidationCertAuthorityFinancialCorrectionId,
                    new ClaimKey("ContractId"),
                    () => this.contractReportsRepository.GetContractId(this.ContractReportId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportRevalidationCertAuthorityFinancialCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
