using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Contracts;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Linq;

namespace Eumis.ApplicationServices.Services.FIReimbursedAmount
{
    public class FIReimbursedAmountService : IFIReimbursedAmountService
    {
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;

        public FIReimbursedAmountService(
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository)
        {
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
        }

        public bool CanCreateFIReimbursedAmount(int userId, Domain.Contracts.Contract contract)
        {
            var programmeId = this.contractsRepository.GetProgrammeId(contract.ContractId);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(userId, MonitoringFinancialControlPermissions.CanWriteFinancial);
            return programmeIds.Contains(programmeId) && contract.ContractStatus == ContractStatus.Entered;
        }
    }
}
