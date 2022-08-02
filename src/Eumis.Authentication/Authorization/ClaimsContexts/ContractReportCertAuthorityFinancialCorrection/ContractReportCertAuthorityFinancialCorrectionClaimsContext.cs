using Autofac.Features.AttributeFilters;
using Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertAuthorityFinancialCorrection
{
    internal class ContractReportCertAuthorityFinancialCorrectionClaimsContext : ClaimsContext, IContractReportCertAuthorityFinancialCorrectionClaimsContext
    {
        private int contractReportCertAuthorityFinancialCorrectionId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportCertAuthorityFinancialCorrectionsRepository contractReportCertAuthorityFinancialCorrectionsRepository;

        public ContractReportCertAuthorityFinancialCorrectionClaimsContext(
            int contractReportCertAuthorityFinancialCorrectionId,
            [KeyFilter(ClaimsCaches.ContractReportCertAuthorityFinancialCorrection)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportCertAuthorityFinancialCorrectionsRepository contractReportCertAuthorityFinancialCorrectionsRepository)
            : base(claimsCache)
        {
            this.contractReportCertAuthorityFinancialCorrectionId = contractReportCertAuthorityFinancialCorrectionId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportCertAuthorityFinancialCorrectionsRepository = contractReportCertAuthorityFinancialCorrectionsRepository;
        }

        public int ContractReportCertAuthorityFinancialCorrectionId
        {
            get
            {
                return this.contractReportCertAuthorityFinancialCorrectionId;
            }
        }

        public int ContractReportId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCertAuthorityFinancialCorrectionId,
                    new ClaimKey("ContractReportId"),
                    () => this.contractReportCertAuthorityFinancialCorrectionsRepository.GetContractReportId(this.contractReportCertAuthorityFinancialCorrectionId));
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCertAuthorityFinancialCorrectionId,
                    new ClaimKey("ContractId"),
                    () => this.contractReportsRepository.GetContractId(this.ContractReportId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCertAuthorityFinancialCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
