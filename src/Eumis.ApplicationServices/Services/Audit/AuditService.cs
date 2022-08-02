using System;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Audits;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.ApplicationServices.Services.Audit
{
    public class AuditService : IAuditService
    {
        private IPermissionsRepository permissionsRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IProceduresRepository proceduresRepository;
        private IContractsRepository contractsRepository;
        private IUnitOfWork unitOfWork;

        public AuditService(
            IPermissionsRepository permissionsRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IProceduresRepository proceduresRepository,
            IContractsRepository contractsRepository,
            IUnitOfWork unitOfWork)
        {
            this.permissionsRepository = permissionsRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractsRepository = contractsRepository;
            this.unitOfWork = unitOfWork;
        }

        public bool CanCreate(int userId, int programmeId, int? contractId, AuditLevel level)
        {
            var programmeIds = Array.Empty<int>();
            var canCreate = programmeIds.Contains(programmeId);

            if (canCreate && level == AuditLevel.ContractContract)
            {
                var contract = this.contractsRepository.Find(contractId.Value);

                canCreate = contract.ProgrammeId == programmeId && contract.ContractStatus == ContractStatus.Entered;
            }

            return canCreate;
        }

        public void CreateItems(Domain.Audits.Audit audit, int userId, int[] itemIds)
        {
            foreach (var itemId in itemIds)
            {
                bool canCreate = true;

                switch (audit.Level)
                {
                    case AuditLevel.ProgrammePriority:
                        canCreate = this.programmePrioritiesRepository.GetProgrammeId(itemId) == audit.ProgrammeId;
                        break;
                    case AuditLevel.Procedure:
                        var programmeIds = this.proceduresRepository.GetProcedureProgrammeIds(itemId);
                        var procStatus = this.proceduresRepository.GetProcedureStatus(itemId);

                        canCreate = programmeIds.Contains(audit.ProgrammeId)
                            && Procedure.EvalSessionOrProjectCreationStatuses.Contains(procStatus);
                        break;
                    case AuditLevel.Contract:
                        var contractStatus = this.contractsRepository.GetContractStatus(itemId);

                        canCreate = this.contractsRepository.GetProgrammeId(itemId) == audit.ProgrammeId &&
                            contractStatus == ContractStatus.Entered;
                        break;
                    case AuditLevel.ContractContract:
                        canCreate = this.contractsRepository.GetContractContractContractId(itemId) == audit.ContractId.Value;
                        break;
                }

                if (!canCreate)
                {
                    throw new InvalidOperationException(string.Format("Cannot create audit item with id {0}", itemId));
                }

                audit.AddAuditLevelItem(itemId);
            }

            this.unitOfWork.Save();
        }
    }
}
