using Autofac.Features.AttributeFilters;
using Eumis.Data.ContractReportFinancialRevalidations.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialRevalidation
{
    internal class ContractReportFinancialRevalidationClaimsContext : ClaimsContext, IContractReportFinancialRevalidationClaimsContext
    {
        private int contractReportRevalidationId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository;

        public ContractReportFinancialRevalidationClaimsContext(
            int contractReportRevalidationId,
            [KeyFilter(ClaimsCaches.ContractReportFinancialRevalidation)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository)
            : base(claimsCache)
        {
            this.contractReportRevalidationId = contractReportRevalidationId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialRevalidationsRepository = contractReportFinancialRevalidationsRepository;
        }

        public int ContractReportRevalidationId
        {
            get
            {
                return this.contractReportRevalidationId;
            }
        }

        public int ContractReportId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportRevalidationId,
                    new ClaimKey("ContractReportId"),
                    () => this.contractReportFinancialRevalidationsRepository.GetContractReportId(this.contractReportRevalidationId));
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportRevalidationId,
                    new ClaimKey("ContractId"),
                    () => this.contractReportsRepository.GetContractId(this.ContractReportId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportRevalidationId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
