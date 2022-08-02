using Autofac.Features.AttributeFilters;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReport
{
    internal class ContractReportClaimsContext : ClaimsContext, IContractReportClaimsContext
    {
        private int contractReportId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;

        public ContractReportClaimsContext(
            int contractReportId,
            [KeyFilter(ClaimsCaches.ContractReport)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository)
            : base(claimsCache)
        {
            this.contractReportId = contractReportId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
        }

        public int ContractReportId
        {
            get
            {
                return this.contractReportId;
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportId,
                    new ClaimKey("ContractId"),
                    () => this.contractReportsRepository.GetContractId(this.contractReportId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractReportId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
