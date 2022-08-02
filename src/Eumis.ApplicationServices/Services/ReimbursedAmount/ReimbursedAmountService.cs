using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Contracts;
using Eumis.Domain.Debts;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ReimbursedAmount
{
    public class ReimbursedAmountService : IReimbursedAmountService
    {
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;

        public ReimbursedAmountService(
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository)
        {
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
        }

        public bool CanCreateDebtReimbursedAmount(int userId, Domain.Debts.ContractDebt contractDebt)
        {
            var programmeId = this.contractsRepository.GetProgrammeId(contractDebt.ContractId);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(userId, MonitoringFinancialControlPermissions.CanWriteFinancial);
            return programmeIds.Contains(programmeId) && contractDebt.Status == ContractDebtStatus.Entered;
        }

        public bool CanCreateContractReimbursedAmount(int userId, Domain.Contracts.Contract contract)
        {
            var programmeId = this.contractsRepository.GetProgrammeId(contract.ContractId);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(userId, MonitoringFinancialControlPermissions.CanWriteFinancial);
            return programmeIds.Contains(programmeId) && contract.ContractStatus == ContractStatus.Entered;
        }
    }
}
