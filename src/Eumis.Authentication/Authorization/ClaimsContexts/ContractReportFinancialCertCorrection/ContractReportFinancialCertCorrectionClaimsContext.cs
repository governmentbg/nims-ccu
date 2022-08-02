using Autofac.Features.AttributeFilters;
using Eumis.Data.ContractReportFinancialCertCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialCertCorrection
{
    internal class ContractReportFinancialCertCorrectionClaimsContext : ClaimsContext, IContractReportFinancialCertCorrectionClaimsContext
    {
        private int contractReportCertCorrectionId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository;

        public ContractReportFinancialCertCorrectionClaimsContext(
            int contractReportCertCorrectionId,
            [KeyFilter(ClaimsCaches.ContractReportFinancialCertCorrection)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository)
            : base(claimsCache)
        {
            this.contractReportCertCorrectionId = contractReportCertCorrectionId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialCertCorrectionsRepository = contractReportFinancialCertCorrectionsRepository;
        }

        public int ContractReportCertCorrectionId
        {
            get
            {
                return this.contractReportCertCorrectionId;
            }
        }

        public int ContractReportId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCertCorrectionId,
                    new ClaimKey("ContractReportId"),
                    () => this.contractReportFinancialCertCorrectionsRepository.GetContractReportId(this.contractReportCertCorrectionId));
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCertCorrectionId,
                    new ClaimKey("ContractId"),
                    () => this.contractReportsRepository.GetContractId(this.ContractReportId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCertCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
