using Autofac.Features.AttributeFilters;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialCorrection
{
    internal class ContractReportFinancialCorrectionClaimsContext : ClaimsContext, IContractReportFinancialCorrectionClaimsContext
    {
        private int contractReportCorrectionId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository;

        public ContractReportFinancialCorrectionClaimsContext(
            int contractReportCorrectionId,
            [KeyFilter(ClaimsCaches.ContractReportFinancialCorrection)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository)
            : base(claimsCache)
        {
            this.contractReportCorrectionId = contractReportCorrectionId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialCorrectionsRepository = contractReportFinancialCorrectionsRepository;
        }

        public int ContractReportCorrectionId
        {
            get
            {
                return this.contractReportCorrectionId;
            }
        }

        public int ContractReportId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCorrectionId,
                    new ClaimKey("ContractReportId"),
                    () => this.contractReportFinancialCorrectionsRepository.GetContractReportId(this.contractReportCorrectionId));
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCorrectionId,
                    new ClaimKey("ContractId"),
                    () => this.contractReportsRepository.GetContractId(this.ContractReportId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
